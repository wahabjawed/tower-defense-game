 
sampler2D input ;

 float top;
 
 float4 transform(float2 uv : TEXCOORD) : COLOR 
 { 
		float bottom = 1 - top; 
		float2 tuv = float2((uv.y - top) / (bottom - top), uv.x);
 
	 float ty = tuv.y; 
	 if (ty > 0.5) 
	 { 
		ty = 1 - ty; 
	 }
	
	 float left = top * ty; 
	 float right = 1 - left;         
	
	 if (uv.x >= left && uv.x <= right) 
	 { 
    
		 float tx = lerp(0, 1, (tuv.x - left) / (right - left)); 
		
	   return tex2D(input, float2(tuv.y, tx)); 
 } 
	return 0; 
 } 
 
 float4 main(float2 uv : TEXCOORD) : COLOR  
 {          
	 float bottom = 1 - top; 
	 if(uv.y > top && uv.y < bottom) 
	 { 
		return transform(uv); 
	 } 
 
	 return 0; 
 }

 technique hit
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 main();
		
	}
}