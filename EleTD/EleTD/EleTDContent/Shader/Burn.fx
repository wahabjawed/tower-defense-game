sampler s0;

float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0,coords);
    color = color*1.5;
	return color;

}

float4 PixelBlurFunction(float2 coords: TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0,coords); 
	color += tex2D(s0,float2(coords.x+0.02,coords.y+0.02));
	color += tex2D(s0,float2(coords.x-0.02,coords.y-0.02));
	color = color/3;
	return color;

}

float4 PixelGlowFunction(float2 coords: TEXCOORD0) : COLOR0
{
	float4 color = tex2D( s0, coords) * 0.1;
	color.r += tex2D( s0, coords ) * 0.3;
	color.b += tex2D( s0, coords ) * 0.4;
	color.g += tex2D( s0, coords ) * 0.25;
	return color;

}


	
	
	

technique hit
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}

	pass Pass2
	{
		PixelShader = compile ps_2_0 PixelBlurFunction();
	}

	pass Pass3
	{
		PixelShader = compile ps_2_0 PixelGlowFunction();
	}
}