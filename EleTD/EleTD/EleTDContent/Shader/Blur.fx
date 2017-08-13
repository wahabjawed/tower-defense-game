sampler s0;

float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0,coords); 
	color += tex2D(s0,float2(coords.x+0.02,coords.y+0.02));
	color += tex2D(s0,float2(coords.x-0.02,coords.y-0.02));
	color = color/3;
	return color;

}

technique hit
{
	pass Pass1
	{	
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}