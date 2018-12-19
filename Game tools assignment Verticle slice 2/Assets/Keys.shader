Shader "Costumized/Keys" {
	Properties

	{
		_Color("Main color", color) = (1,0,1,0)
		_Scale("Scale", Range(0,1)) = 0
	}

		SubShader
	{
		pass
	{

		CGPROGRAM


		 #pragma vertex vertexFunction
		 #pragma fragment fragmentFunciton

		 #include "UnityCG.cginc"



			struct appdata
		{
			float4 pos: POSITION;
			float3 normal : NORMAL;

		};
		struct v2f
		{
			float4 pos: POSITION;
			float3 normal : NORMAL;
		};

		fixed4 _Color;
		float _Scale;

		v2f vertexFunction(appdata v)
		{
			v2f o;


			o.pos = UnityObjectToClipPos(v.pos);

			o.normal = UnityObjectToWorldNormal(v.normal);
			o.pos = o.pos + float4(_Scale* abs(sin(_Time.w)) * o.normal, 1);

			return o;
		}

		fixed4 fragmentFunciton(v2f i) : TARGET
		{
			return fixed4(i.normal,1);
		}
		ENDCG
	}

	}
}

