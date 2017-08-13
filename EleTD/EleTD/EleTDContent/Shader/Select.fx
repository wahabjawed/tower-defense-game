sampler s0;


float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{   float4 pixelColor = tex2D(s0, coords);
    pixelColor.rgb /= pixelColor.a;
    
    // Apply contrast.
    pixelColor.rgb = ((pixelColor.rgb - 0.5f) * max(1, 0)) + 0.5f;
    
    // Apply brightness.
    pixelColor.rgb += 0.150;
    
    // Return final pixel color.
    pixelColor.rgb *= pixelColor.a;
    return pixelColor;
}


technique hit
{
	pass Pass1
	{	
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}