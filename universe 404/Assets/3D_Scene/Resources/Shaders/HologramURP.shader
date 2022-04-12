Shader "Custom/HologramURP"
{
    Properties
    {
        _MainColor("颜色", COLOR) = (1,1,1,1)
        _Alpha("透明度",float) = 0.7
        _MainTex ("Texture", 2D) = "white" {}
        _DirVertex("DirVertex",Vector) = (0,1,0,0)
        _Glitch("Glitch",float) = 0
        _GlitchIntense("GlitchIntense",float) = 0.1
        _GlitchSpped("_GlitchSpped",float) = 8
        _Scanline("Scanline",float) = 0
        _ScanTiling("_ScanTiling",float) = 100
        _ScanSpeed("ScanSpeed",float) = -1.58

        _GlowSpeed("GlowSpeed",float) = 10
        _GlowTiling("GlowTiling",float) = 0.05
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Tags
            {
                "LightMode" = "UniversalForward"
            }
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 worldVertex : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _DirVertex;
            float4 _MainColor;
            float _GlitchIntense;
            float _GlitchSpped;
            float _ScanSpeed;
            float _ScanTiling;
            float _Alpha;
            float _GlowSpeed;
            float _GlowTiling;

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.x += _GlitchIntense * step(0.5, sin(_Time.z + v.vertex.y)) * step(0.99, sin(_Time.y*0.5 * _GlitchSpped));
                VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
                o.vertex = vertexInput.positionCS;
                o.worldVertex = float4(vertexInput.positionWS,1);
                //抖动偏移
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 mainCol = tex2D(_MainTex, i.uv);
                float dirVertex = (dot(i.worldVertex, normalize(float4(_DirVertex.xyz,1.0)))+1)/2;
                //扫描线，为什么只用frac呢？
                float scanline = step(frac(dirVertex*_ScanTiling+_ScanSpeed*_Time.w),0.5)* _Alpha;
                //边缘光
                //float4 rim.
                float glow = 0.0;
                glow = frac(dirVertex * _GlowTiling - _Time.x * _GlowSpeed);
                float alpha = _Alpha;
                float4 col= float4(mainCol.rgb* _MainColor.rgb+(mainCol.rgb * _MainColor.rgb* glow*0.2),1);
                col.a = mainCol.a*alpha*( scanline);
                return col;
            }
            ENDHLSL
        }
    }
}
