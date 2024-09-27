Shader "Custom/Water"
{
    Properties
    {
        _Color ("Water Color", Color) = (0.0, 0.5, 0.7, 0.5) // ���� �⺻ ����
        _Glossiness ("Smoothness", Range(0,1)) = 0.8 // ǥ���� ������ ����
        _Metallic ("Metallic", Range(0,1)) = 0.0 // �ݼӼ� �ݻ� ����
        _WaveSpeed ("Wave Speed", Range(0, 1)) = 0.3 // ������ �ӵ�
        _WaveAmplitude ("Wave Amplitude", Range(0, 0.1)) = 0.05 // ������ ����
        _MainTex ("Water Texture", 2D) = "white" {} // �� ǥ�� �ؽ�ó
        _BumpMap ("Normal Map", 2D) = "bump" {} // ������ ǥ���� �븻 ��
    }
    
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200
        
        Blend SrcAlpha OneMinusSrcAlpha // ���� ����
        Cull Back // ���� ���� �׸��� ����
        ZWrite Off // ���� ���ۿ� ���� ���� (���� ������Ʈ)
        
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
            // ���� �������� �ð��� ���� UV�� �д�(panning)�Ͽ� ǥ��
            float wave = sin((IN.worldPos.x + _Time.y * _WaveSpeed) * 2.0) * _WaveAmplitude;

            // �ؽ�ó �� �븻�� ���ø�
            half4 tex = tex2D(_MainTex, IN.uv_MainTex);
            half4 bump = tex2D(_BumpMap, IN.uv_BumpMap + wave);

            // �⺻ ���� ����
            o.Albedo = _Color.rgb * tex.rgb;

            // �븻�� ���� (���ῡ ���� ����)
            o.Normal = UnpackNormal(bump);

            // �ݻ� �� ���� ����
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;

            // ���� ���� ���� ���� ����
            o.Alpha = _Color.a * tex.a;
        }
        ENDCG
    }
    
    FallBack "Transparent/Cutout/Diffuse"
}
