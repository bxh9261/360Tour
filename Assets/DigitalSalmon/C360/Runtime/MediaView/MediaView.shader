Shader "Digital Salmon/C360/Media View" {
    Properties {
		_MainTex("MainTex", 2D) = "black" {}
        _Yaw ("Yaw", Float ) = 0

		[MaterialToggle] _YFlip("YFlip", Float) = 0
        [MaterialToggle] _Stereoscopic ("Stereoscopic", Float ) = 0
		[Enum(Equirectangular,0,VR180,1)] _Projection("Projection", int) = 0

		_ProjectionMaskSmoothing ("Projection Mask Smoothing", float) = 0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        Pass {
            Name "FORWARD"
			Cull Front
			ZWrite Off
			
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
			#include "Assets/DigitalSalmon/C360/Internal/Include/DigitalSalmon.C360.cginc"
            
            struct VertexInput {
                float4 vertex : POSITION;
            };

            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
            };
            
			uniform sampler2D _MainTex; uniform float4 _MainTex_ST;

			uniform float _Yaw;
			uniform fixed _Stereoscopic;
			uniform int _Projection;
			uniform float _ProjectionMaskSmoothing;
			uniform fixed _YFlip;

            ProjectionSettings getProjectionSettings()
            {
                ProjectionSettings projectionSettings;
                projectionSettings.Projection = _Projection;
                projectionSettings.Stereo = _Stereoscopic; 
                projectionSettings.Yaw = _Yaw;
                projectionSettings.YFlip = _YFlip;
                projectionSettings.ProjectionMaskSmoothing = _ProjectionMaskSmoothing;
                return projectionSettings;
            }
            
            VertexOutput vert (VertexInput v)
            {
                VertexOutput o = (VertexOutput)0;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex);  
                return o;
            }

            float4 frag(VertexOutput i) : COLOR {
                const float3 dir = normalize(i.posWorld.rgb);                
                const ProjectionSettings settings = getProjectionSettings();

                const float2 uv = calculateProjectionUv(dir, settings);
                const float mask = projectionMask(dir, settings);
				
				return tex2D(_MainTex, uv) * mask;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
