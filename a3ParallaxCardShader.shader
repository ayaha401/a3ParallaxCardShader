Shader "a3ParallaxCardShader/Parallax"
{
    Properties
    {
        _StackColor("StackColor", color) = (1.0, 1.0, 1.0 ,1.0)

        _ParallaxScale("Parallax Scale", Range(0.0, 1.0)) = 1.0

        _Layer0 ("Layer0", 2D) = "white" {}
        _Layer0Height("Layer0Hegiht", Range(0.0, 1.0)) = 1.0
        _Layer0StackColor("Layer0StackColor", color) = (1.0, 1.0, 1.0, 1.0)

        [Toggle(_USE_LAYER_ONE)] _UseLayer1("UseLayer1", Float) = 0
        _Layer1("Layer1", 2D) = "white" {}
        _Layer1Height("Layer1Hegiht", Range(0.0, 1.0)) = 1.0
        _Layer1StackColor("Layer1StackColor", color) = (1.0, 1.0, 1.0, 1.0)

        [Toggle(_USE_LAYER_TWO)] _UseLayer2("UseLayer2", Float) = 0
        _Layer2("Layer2", 2D) = "white" {}
        _Layer2Height("Layer2Hegiht", Range(0.0, 1.0)) = 1.0
        _Layer2StackColor("Layer2StackColor", color) = (1.0, 1.0, 1.0, 1.0)

        [Toggle(_USE_LAYER_THREE)] _UseLayer3("UseLayer3", Float) = 0
        _Layer3("Layer3", 2D) = "white" {}
        _Layer3Height("Layer3Hegiht", Range(0.0, 1.0)) = 1.0
        _Layer3StackColor("Layer3StackColor", color) = (1.0, 1.0, 1.0, 1.0)

        [Toggle(_USE_LAYER_FOUR)] _UseLayer4("UseLayer4", Float) = 0
        _Layer4("Layer4", 2D) = "white" {}
        _Layer4Height("Layer4Hegiht", Range(0.0, 1.0)) = 1.0
        _Layer4StackColor("Layer4StackColor", color) = (1.0, 1.0, 1.0, 1.0)

        _BackTex("BackTexture", 2D) = "white" {}
        _BackTexStackColor("BackTexStackColor", color) = (1.0, 1.0, 1.0, 1.0)

        _DissolveTex("DissolveTexture", 2D) = "white" {}
        _DissolveAmount("DissolveAmount", Range(0.0, 1.0)) = 0.0

        [Toggle(_USE_EDGE)] _UseEdge("UseEdge", Float) = 0
        [HDR]_EdgeColor("EdgeColor", color) = (1.0, 1.0, 1.0,1.0)
        _EdgeRange("EdgeRange", Range(0.0, 0.5)) = 0.1
        _EdgeBlur("EdgeBlur", Range(0.001, 0.3)) = 0.1

    }
    SubShader
    {
        Tags 
        { 
            "RenderType"="Transparent"
        }
        Cull Off
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma shader_feature _USE_LAYER_ONE
            #pragma shader_feature _USE_LAYER_TWO
            #pragma shader_feature _USE_LAYER_THREE
            #pragma shader_feature _USE_LAYER_FOUR
            #pragma shader_feature _USE_EDGE

            #pragma target 4.0

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;

                //=====================================================
                float3 viewDir : TEXCOORD1;
                float3 normal : NORMAL;
            };

            sampler2D _Layer0;
            float _Layer0Height;
            float4 _Layer0StackColor;

            #ifdef _USE_LAYER_ONE
                sampler2D _Layer1;
                float _Layer1Height;
                float4 _Layer1StackColor;
            #endif

            #ifdef _USE_LAYER_TWO
                sampler2D _Layer2;
                float _Layer2Height;
                float4 _Layer2StackColor;
            #endif

            #ifdef _USE_LAYER_THREE
                sampler2D _Layer3;
                float _Layer3Height;
                float4 _Layer3StackColor;
            #endif

            #ifdef _USE_LAYER_FOUR
                sampler2D _Layer4;
                float _Layer4Height;
                float4 _Layer4StackColor;
            #endif

            sampler2D _BackTex;
            float4 _BackTexStackColor;

            float _ParallaxScale;

            float3 _StackColor;

            sampler2D _DissolveTex;
            float _DissolveAmount;

            #if _USE_EDGE
                float3 _EdgeColor;
                float _EdgeRange;
                float _EdgeBlur;
            #endif

            float2 Parallax(float height, float parallaxScale, float3 viewDir)
            {
                return (height - 1.0) * (viewDir.xy / viewDir.z) * parallaxScale;
            }

            float2 getUV(float3 viewDir, float layerHeight, float parallaxScale, float2 uv)
            {
                viewDir = normalize(viewDir);
                float2 offset = Parallax(layerHeight, parallaxScale, viewDir);
                uv += offset;
                return uv;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                TANGENT_SPACE_ROTATION;
                o.viewDir = mul(rotation, ObjSpaceViewDir(v.vertex));
                return o;
            }

            float4 frag (v2f i, float facing : VFACE) : SV_Target
            {
                // UV計算
                float2 uv0 = getUV(i.viewDir, _Layer0Height, _ParallaxScale, i.uv);
                
                #ifdef _USE_LAYER_ONE
                    float2 uv1 = getUV(i.viewDir, _Layer1Height, _ParallaxScale, i.uv);
                #endif

                #ifdef _USE_LAYER_TWO
                    float2 uv2 = getUV(i.viewDir, _Layer2Height, _ParallaxScale, i.uv);
                #endif

                #ifdef _USE_LAYER_THREE
                    float2 uv3 = getUV(i.viewDir, _Layer3Height, _ParallaxScale, i.uv);
                #endif

                #ifdef _USE_LAYER_FOUR
                    float2 uv4 = getUV(i.viewDir, _Layer4Height, _ParallaxScale, i.uv);
                #endif


                // ディゾルブ計算
                float dissolve = tex2D(_DissolveTex, i.uv).r;
                dissolve *= 0.999;

                float isVisible = dissolve - _DissolveAmount;
                clip(isVisible);

                // Edge計算
                #if _USE_EDGE
                    float edge = smoothstep(_EdgeRange + _EdgeBlur, _EdgeRange, isVisible);
                    float4 isEdge = float4(edge * _EdgeColor.r, edge * _EdgeColor.g, edge * _EdgeColor.b, edge);
                #endif

                // tex2D計算
                float4 layer0, layer1, layer2, layer3, layer4, backTex;
                layer0 = layer1 = layer2 = layer3 = layer4 = backTex = 0.0;

                layer0.a *= 0.0;

                layer0 += tex2D(_Layer0, uv0) * _Layer0StackColor;

                #ifdef _USE_LAYER_ONE
                    layer1 += tex2D(_Layer1, uv1) * _Layer1StackColor;
                #endif

                #ifdef _USE_LAYER_TWO
                    layer2 += tex2D(_Layer2, uv2) * _Layer2StackColor;
                #endif

                #ifdef _USE_LAYER_THREE
                    layer3 += tex2D(_Layer3, uv3) * _Layer3StackColor;
                #endif

                #ifdef _USE_LAYER_FOUR
                    layer4 += tex2D(_Layer4, uv4) * _Layer4StackColor;
                #endif

                backTex += tex2D(_BackTex, i.uv) * _BackTexStackColor;
                
                // 色計算
                float3 color = layer0;
                color = color * (1-layer1.a) + layer1 * layer1.a;
                color = color * (1-layer2.a) + layer2 * layer2.a;
                color = color * (1-layer3.a) + layer3 * layer3.a;
                color = color * (1-layer4.a) + layer4 * layer4.a;
                
                color = color * _StackColor;

                float3 backColor = backTex.rgb;
                backColor = backColor * _StackColor;

                #if _USE_EDGE
                    color = color * (1.0 - isEdge.a) + isEdge * isEdge.a;
                    backColor = backColor * (1.0 - isEdge.a) + isEdge * isEdge.a;
                # endif

                return (facing > 0 ? float4(color, 0.0) : float4(backColor, 1.0));
            }
            ENDCG
        }
    }
    CustomEditor "CardShaderGUI"
}
