Shader "Custom/Water"
{
    Properties
    {
        _Color ("Water Color", Color) = (0.0, 0.5, 0.7, 0.5) // 물의 기본 색상
        _Glossiness ("Smoothness", Range(0,1)) = 0.8 // 표면의 광택을 조절
        _Metallic ("Metallic", Range(0,1)) = 0.0 // 금속성 반사 조절
        _WaveSpeed ("Wave Speed", Range(0, 1)) = 0.3 // 물결의 속도
        _WaveAmplitude ("Wave Amplitude", Range(0, 0.1)) = 0.05 // 물결의 진폭
        _MainTex ("Water Texture", 2D) = "white" {} // 물 표면 텍스처
        _BumpMap ("Normal Map", 2D) = "bump" {} // 물결을 표현할 노말 맵
    }
    
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200
        
        Blend SrcAlpha OneMinusSrcAlpha // 투명도 적용
        Cull Back // 뒤쪽 면을 그리지 않음
        ZWrite Off // 깊이 버퍼에 쓰지 않음 (투명 오브젝트)
        
        CGPROGRAM
        #pragma surface surf Standard alpha:fade

        sampler2D _MainTex;
        sampler2D _BumpMap;
        half4 _Color;
        half _Glossiness;
        half _Metallic;
        float _WaveSpeed;
        float _WaveAmplitude;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float3 worldPos;
        };

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // 물결 움직임을 시간에 따라 UV를 패닝(panning)하여 표현
            float wave = sin((IN.worldPos.x + _Time.y * _WaveSpeed) * 2.0) * _WaveAmplitude;

            // 텍스처 및 노말맵 샘플링
            half4 tex = tex2D(_MainTex, IN.uv_MainTex);
            half4 bump = tex2D(_BumpMap, IN.uv_BumpMap + wave);

            // 기본 색상 적용
            o.Albedo = _Color.rgb * tex.rgb;

            // 노말맵 적용 (물결에 따른 변형)
            o.Normal = UnpackNormal(bump);

            // 반사 및 광택 적용
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;

            // 알파 값을 통해 투명도 적용
            o.Alpha = _Color.a * tex.a;
        }
        ENDCG
    }
    
    FallBack "Transparent/Cutout/Diffuse"
}
