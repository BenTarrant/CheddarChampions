Shader "Custom/Slice" 
{
	Properties
	{
		_Color ("Colour", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	
		_SliceType ("Slicing Type", Range(0, 7)) = 0
		_SliceAngle ("Slice Angle", Float) = 0.1
		_SlicingWidth ("Slicing Width", Range(0, 1)) = 0.5
		_NoSlices ("Number Of Slices", Range(0, 100)) = 5
	}

	SubShader 
	{

		// Decides quieing type and render type.
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent"}
		
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		
		int _SliceType;
		float _SliceAngle;
		float _SlicingWidth;
		float _NoSlices;

		struct Input 
		{
			float2 uv_MainTex;
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			// Diagonal Stripes --> WorldCoordinates
			if (_SliceType == 0)
			{
				clip(frac((IN.worldPos.y + IN.worldPos.x * _SliceAngle) * _NoSlices) - _SlicingWidth);
			}

			// Horizontal Stripes --> WorldCoordinates
			if (_SliceType == 1)
			{
				clip(frac((IN.worldPos.y) * _NoSlices) - _SlicingWidth);
			}

			// Vectical Stripes --> WorldCoordinates
			if (_SliceType == 2)
			{
				clip(frac((IN.worldPos.x) * _NoSlices) - _SlicingWidth);
			}

			// Diagonal Stripes --> UVCoordinates
			if (_SliceType == 3)
			{
				clip(frac((IN.uv_MainTex.y + IN.uv_MainTex.x * _SliceAngle) * _NoSlices) - _SlicingWidth);
			}

			// Horizontal Stripes --> UVCoordinates
			if (_SliceType == 4)
			{
				clip(frac((IN.uv_MainTex.y) * _NoSlices) - _SlicingWidth);
			}

			//Vectical Stripes-- > UVCoordinates
			if (_SliceType == 5)
			{
				clip(frac((IN.uv_MainTex.x) * _NoSlices) - _SlicingWidth);
			}

			// 
			if (_SliceType == 6)
			{
				clip(((IN.worldPos.y + IN.worldPos.x) * _NoSlices) - _SlicingWidth);
			}

			if (_SliceType == 7)
			{
				clip(frac((IN.worldPos.y) * _NoSlices) - _SlicingWidth);
				clip(frac((IN.worldPos.x) * _NoSlices) - _SlicingWidth);
			}

			// Albedo comes from a texture tinted by color.
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}

		ENDCG
	}

	FallBack "Diffuse"
}