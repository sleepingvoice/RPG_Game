Shader "Custom/Transper"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1) // 색상 및 투명도 속성
        _Glossiness ("Smoothness", Range(0,1)) = 0.5 // 표면의 광택을 조절하는 속성
        _Metallic ("Metallic", Range(0,1)) = 0.0 // 금속성 반사를 조절하는 속성
        _MainTex ("Texture", 2D) = "white" {} // 기본 텍스처
    }
    
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha // 알파 블렌딩 (투명도 적용)
        ZWrite Off // 깊이 쓰기를 비활성화해서 투명한 오브젝트가 깊이 버퍼에 기록되지 않도록 함.
        Cull Back // 뒤쪽 면을 그리지 않음

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _Color;
            float _Glossiness;
            float _Metallic;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // 텍스처 샘플링
                half4 texColor = tex2D(_MainTex, i.uv);

                // 텍스처의 색상을 가져오고 알파값을 적용
                half4 finalColor = texColor * _Color;

                // 최종 색상은 알파 값으로 블렌딩됨 (투명도)
                finalColor.a = _Color.a * texColor.a;

                return finalColor;
            }
            ENDCG
        }
    }

    FallBack "Transparent/Cutout/Diffuse"
}
