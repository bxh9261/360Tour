#ifndef DIGITALSALMON_C360
#define DIGITALSALMON_C360

#ifndef TAU
#define TAU 6.28318530718
#endif

#ifndef DEG2RAD
#define DEG2RAD 0.01745329252
#endif

// Types

struct ProjectionSettings
{
    float Yaw;
    int Projection;
    int Stereo;
    int YFlip;
    float ProjectionMaskSmoothing;
};

// Utility

float2 rotate(float2 domain, float angleDegrees)
{
    const float s = sin(angleDegrees * DEG2RAD);
    const float c = cos(angleDegrees * DEG2RAD);
    const float tx = domain.x;
    const float ty = domain.y;
    domain.x = (c * tx) - (s * ty);
    domain.y = (s * tx) + (c * ty);
    return domain;
}

// Projection

float2 calculateEquirectangularProjectionUv(float3 dir, ProjectionSettings settings)
{
    float pitch = (acos(-dir.g) / UNITY_PI);
    float yaw = frac(((atan2(dir.r, dir.b) / TAU)) + settings.Yaw);
    return float2(yaw, pitch);
}

float2 handleEquirectangularUv(float2 uv, ProjectionSettings settings)
{
    if (settings.Stereo)
    {
        uv.y /= 2;
        uv.y += lerp(0.0, 0.5, unity_StereoEyeIndex);
        uv.x = frac(uv.x);
    }
    return uv;
}

float2 handleVr180Uv(float2 uv, ProjectionSettings settings)
{
    uv.x *= 2;

    if (settings.Stereo)
    {
        uv.x /= 2;
        const int eyeIndex = unity_StereoEyeIndex;
        uv.x += lerp(0.5, 0.0, eyeIndex);
        uv.x += 0.25;
    }
    else
    {
        uv.x += 0.5;
    }
    return uv;
}

float2 handleYFlip(float2 uv, ProjectionSettings settings)
{
    if (settings.YFlip)
        uv.y = (1-uv.y);
   
    return uv;
}

float2 calculateProjectionUv(float3 dir, ProjectionSettings settings)
{
    float2 uv = calculateEquirectangularProjectionUv(dir, settings);

    if (settings.Projection == 0)
        uv = handleEquirectangularUv(uv, settings);    

    if (settings.Projection == 1)    
       uv = handleVr180Uv(uv, settings);
    
    uv = handleYFlip(uv, settings);
    
    return frac(uv);
}

// Masking

float maskVr180(float3 dir, ProjectionSettings settings)
{
    float2 dotForward = rotate(float2(0, -1), settings.Yaw * 360);
    float field = dot(dir, float3(dotForward.x, 0, dotForward.y));
    return pow(smoothstep(0, max(0.001, settings.ProjectionMaskSmoothing), field), 2);
    
}

float projectionMask(float3 dir, ProjectionSettings settings)
{
    if (settings.Projection == 1)    
     return maskVr180(dir, settings);
    
    return 1;
}


#endif 
