Shader "Lines/Colored Blended" 
{
	SubShader 
	{ 
		Pass 
		{
           //BindChannels
		   //{
			//	Bind "Color", color 
		   //}
           //Blend SrcAlpha OneMinusSrcAlpha
           //ZWrite Off Cull Off Fog { Mode Off }
		   
		   Material {
                Diffuse (1,1,1,1)
                Ambient (1,1,1,1)
            }
            Lighting On
        }
	}
}