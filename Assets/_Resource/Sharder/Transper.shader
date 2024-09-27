Shader "Custom/Transper"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1) // ���� �� ���� �Ӽ�
        _Glossiness ("Smoothness", Range(0,1)) = 0.5 // ǥ���� ������ �����ϴ� �Ӽ�
        _Metallic ("Metallic", Range(0,1)) = 0.0 // �ݼӼ� �ݻ縦 �����ϴ� �Ӽ�
        _MainTex ("Texture", 2D) = "white" {} // �⺻ �ؽ�ó
    }
    
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha // ���� ���� (���� ����)
        ZWrite Off // ���� ���⸦ ��Ȱ��ȭ�ؼ� ������ ������Ʈ�� ���� ���ۿ� ��ϵ��� �ʵ��� ��.
        Cull Back // ���� ���� �׸��� ����

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
                // �ؽ�ó ���ø�
                half4 texColor = tex2D(_MainTex, i.uv);

                // �ؽ�ó�� ������ �������� ���İ��� ����
                half4 finalColor = texColor * _Color;

                // ���� ������ ���� ������ ������ (����)
                finalColor.a = _Color.a * texColor.a;

                return finalColor;
            }
            ENDCG
        }
    }

    FallBack "Transparent/Cutout/Diffuse"
}
