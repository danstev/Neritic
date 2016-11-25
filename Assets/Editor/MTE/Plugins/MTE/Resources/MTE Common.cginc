﻿#ifndef MTE_COMMON_CGINC_INCLUDED
#define MTE_COMMON_CGINC_INCLUDED

void MTE_SplatmapFinalColor(Input IN, SurfaceOutput o, inout fixed4 color)
{
	color *= o.Alpha;
	UNITY_APPLY_FOG(IN.fogCoord, color);
}

void MTE_SplatmapFinalPrepass(Input IN, SurfaceOutput o, inout fixed4 normalSpec)
{
	normalSpec *= o.Alpha;
}

void MTE_SplatmapFinalGBuffer(Input IN, SurfaceOutput o, inout half4 diffuse, inout half4 specSmoothness, inout half4 normal, inout half4 emission)
{
	diffuse.rgb *= o.Alpha;
	specSmoothness *= o.Alpha;
	normal.rgb *= o.Alpha;
	emission *= o.Alpha;
}


#endif // MTE_COMMON_CGINC_INCLUDED