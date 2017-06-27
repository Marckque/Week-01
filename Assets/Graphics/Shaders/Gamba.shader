// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:1,vtps:0,hqsc:True,nrmq:0,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-5017-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:31242,y:33084,ptovrint:False,ptlb:CookedColor,ptin:_CookedColor,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.7843137,c2:0.07843139,c3:0.07843139,c4:1;n:type:ShaderForge.SFN_Lerp,id:8181,x:31975,y:32685,varname:node_8181,prsc:2|A-2637-RGB,B-618-OUT,T-4420-OUT;n:type:ShaderForge.SFN_Color,id:2637,x:31780,y:32503,ptovrint:False,ptlb:RawColor,ptin:_RawColor,varname:_CookedColor_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.7843137,c2:0.6574394,c3:0.6574394,c4:1;n:type:ShaderForge.SFN_Lerp,id:1467,x:32106,y:33126,varname:node_1467,prsc:2|A-7241-RGB,B-2917-RGB,T-9123-OUT;n:type:ShaderForge.SFN_Color,id:2917,x:31677,y:33348,ptovrint:False,ptlb:BurntColor,ptin:_BurntColor,varname:_CookedColor_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_If,id:5017,x:32506,y:32808,varname:node_5017,prsc:2|A-7360-OUT,B-4963-OUT,GT-1467-OUT,EQ-7241-RGB,LT-8181-OUT;n:type:ShaderForge.SFN_Relay,id:618,x:31804,y:32664,varname:node_618,prsc:2|IN-7241-RGB;n:type:ShaderForge.SFN_RemapRange,id:4420,x:31767,y:32726,varname:node_4420,prsc:2,frmn:0,frmx:0.5,tomn:0,tomx:1|IN-1004-OUT;n:type:ShaderForge.SFN_RemapRange,id:9123,x:31676,y:33161,varname:node_9123,prsc:2,frmn:0.5,frmx:1,tomn:0,tomx:1|IN-1004-OUT;n:type:ShaderForge.SFN_Vector1,id:4963,x:32211,y:32616,varname:node_4963,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Relay,id:7360,x:32359,y:32807,varname:node_7360,prsc:2|IN-1004-OUT;n:type:ShaderForge.SFN_Vector1,id:1004,x:31250,y:32966,varname:node_1004,prsc:2,v1:0;proporder:2637-7241-2917;pass:END;sub:END;*/

Shader "Shader Forge/Gamba" {
    Properties {
        _RawColor ("RawColor", Color) = (0.7843137,0.6574394,0.6574394,1)
        _CookedColor ("CookedColor", Color) = (0.7843137,0.07843139,0.07843139,1)
        _BurntColor ("BurntColor", Color) = (0,0,0,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "DEFERRED"
            Tags {
                "LightMode"="Deferred"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_DEFERRED
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile ___ UNITY_HDR_ON
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _CookedColor;
            uniform float4 _RawColor;
            uniform float4 _BurntColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            void frag(
                VertexOutput i,
                out half4 outDiffuse : SV_Target0,
                out half4 outSpecSmoothness : SV_Target1,
                out half4 outNormal : SV_Target2,
                out half4 outEmission : SV_Target3 )
            {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float node_1004 = 0.0;
                float node_5017_if_leA = step(node_1004,0.5);
                float node_5017_if_leB = step(0.5,node_1004);
                float3 emissive = lerp((node_5017_if_leA*lerp(_RawColor.rgb,_CookedColor.rgb,(node_1004*2.0+0.0)))+(node_5017_if_leB*lerp(_CookedColor.rgb,_BurntColor.rgb,(node_1004*2.0+-1.0))),_CookedColor.rgb,node_5017_if_leA*node_5017_if_leB);
                float3 finalColor = emissive;
                outDiffuse = half4( 0, 0, 0, 1 );
                outSpecSmoothness = half4(0,0,0,0);
                outNormal = half4( normalDirection * 0.5 + 0.5, 1 );
                outEmission = half4( lerp((node_5017_if_leA*lerp(_RawColor.rgb,_CookedColor.rgb,(node_1004*2.0+0.0)))+(node_5017_if_leB*lerp(_CookedColor.rgb,_BurntColor.rgb,(node_1004*2.0+-1.0))),_CookedColor.rgb,node_5017_if_leA*node_5017_if_leB), 1 );
                #ifndef UNITY_HDR_ON
                    outEmission.rgb = exp2(-outEmission.rgb);
                #endif
            }
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _CookedColor;
            uniform float4 _RawColor;
            uniform float4 _BurntColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float node_1004 = 0.0;
                float node_5017_if_leA = step(node_1004,0.5);
                float node_5017_if_leB = step(0.5,node_1004);
                float3 emissive = lerp((node_5017_if_leA*lerp(_RawColor.rgb,_CookedColor.rgb,(node_1004*2.0+0.0)))+(node_5017_if_leB*lerp(_CookedColor.rgb,_BurntColor.rgb,(node_1004*2.0+-1.0))),_CookedColor.rgb,node_5017_if_leA*node_5017_if_leB);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
