sampler s0;
float BlurAmount;
float Center;
float Brightness;


float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
	float4 c = 0;    
	coords -= Center;

	for (int i = 0; i < 15; i++)
    {
		float scale = 1.0 + BlurAmount * (i / 14.0);
		c += tex2D(s0, coords * scale + Center);

	
	
	}
	
   c.r += 0.5;

		
	c /= 15;
	return c;
}

technique hit
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}