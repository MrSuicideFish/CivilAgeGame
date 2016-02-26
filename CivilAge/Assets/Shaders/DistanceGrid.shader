// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:1,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:9361,x:33761,y:32701,varname:node_9361,prsc:2|emission-312-OUT,custl-5085-OUT,alpha-6545-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:8068,x:32734,y:33086,varname:node_8068,prsc:2;n:type:ShaderForge.SFN_LightColor,id:3406,x:32734,y:32952,varname:node_3406,prsc:2;n:type:ShaderForge.SFN_LightVector,id:6869,x:31858,y:32654,varname:node_6869,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:9684,x:31858,y:32782,prsc:2,pt:True;n:type:ShaderForge.SFN_HalfVector,id:9471,x:31858,y:32933,varname:node_9471,prsc:2;n:type:ShaderForge.SFN_Dot,id:7782,x:32070,y:32697,cmnt:Lambert,varname:node_7782,prsc:2,dt:1|A-6869-OUT,B-9684-OUT;n:type:ShaderForge.SFN_Dot,id:3269,x:32070,y:32871,cmnt:Blinn-Phong,varname:node_3269,prsc:2,dt:1|A-9684-OUT,B-9471-OUT;n:type:ShaderForge.SFN_Multiply,id:2746,x:32465,y:32866,cmnt:Specular Contribution,varname:node_2746,prsc:2|A-7782-OUT,B-5267-OUT,C-4865-RGB;n:type:ShaderForge.SFN_Tex2d,id:851,x:32070,y:32349,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_851,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1941,x:32465,y:32693,cmnt:Diffuse Contribution,varname:node_1941,prsc:2|A-544-OUT,B-7782-OUT;n:type:ShaderForge.SFN_Color,id:5927,x:32070,y:32534,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_5927,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Exp,id:1700,x:32070,y:33054,varname:node_1700,prsc:2,et:1|IN-9978-OUT;n:type:ShaderForge.SFN_Slider,id:5328,x:31529,y:33056,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_5328,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Power,id:5267,x:32268,y:32940,varname:node_5267,prsc:2|VAL-3269-OUT,EXP-1700-OUT;n:type:ShaderForge.SFN_Add,id:2159,x:32734,y:32812,cmnt:Combine,varname:node_2159,prsc:2|A-1941-OUT,B-2746-OUT;n:type:ShaderForge.SFN_Multiply,id:5085,x:32979,y:32952,cmnt:Attenuate and Color,varname:node_5085,prsc:2|A-2159-OUT,B-3406-RGB,C-8068-OUT;n:type:ShaderForge.SFN_ConstantLerp,id:9978,x:31858,y:33056,varname:node_9978,prsc:2,a:1,b:11|IN-5328-OUT;n:type:ShaderForge.SFN_Color,id:4865,x:32268,y:33095,ptovrint:False,ptlb:Spec Color,ptin:_SpecColor,varname:node_4865,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_AmbientLight,id:7528,x:32734,y:32646,varname:node_7528,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2460,x:32927,y:32598,cmnt:Ambient Light,varname:node_2460,prsc:2|A-544-OUT,B-7528-RGB;n:type:ShaderForge.SFN_Multiply,id:544,x:32268,y:32448,cmnt:Diffuse Color,varname:node_544,prsc:2|A-851-RGB,B-5927-RGB;n:type:ShaderForge.SFN_Tex2d,id:4353,x:30308,y:34131,ptovrint:False,ptlb:GridAlpha,ptin:_GridAlpha,varname:node_9773,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:529239097d02f9f42b0ddd436c6fcbb0,ntxv:3,isnm:False|UVIN-2101-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:60,x:30308,y:33720,varname:node_60,prsc:2;n:type:ShaderForge.SFN_ComponentMask,id:3520,x:30308,y:33844,varname:node_3520,prsc:2,cc1:0,cc2:2,cc3:-1,cc4:-1|IN-60-XYZ;n:type:ShaderForge.SFN_Multiply,id:2101,x:30308,y:33987,varname:node_2101,prsc:2|A-3520-OUT,B-6534-OUT;n:type:ShaderForge.SFN_Vector1,id:6534,x:30141,y:34007,varname:node_6534,prsc:2,v1:1;n:type:ShaderForge.SFN_Color,id:8038,x:31798,y:33359,ptovrint:False,ptlb:GridColor,ptin:_GridColor,varname:node_2823,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:312,x:32057,y:33342,varname:node_312,prsc:2|A-8038-RGB,B-2471-OUT;n:type:ShaderForge.SFN_Vector1,id:2471,x:31896,y:33508,varname:node_2471,prsc:2,v1:10;n:type:ShaderForge.SFN_Distance,id:5245,x:31791,y:33748,varname:node_5245,prsc:2|A-60-XYZ,B-4780-OUT;n:type:ShaderForge.SFN_ViewPosition,id:8985,x:31139,y:33815,varname:node_8985,prsc:2;n:type:ShaderForge.SFN_Divide,id:7952,x:32034,y:33748,varname:node_7952,prsc:2|A-5245-OUT,B-9049-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9049,x:31791,y:33897,ptovrint:False,ptlb:Anis,ptin:_Anis,varname:node_7719,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4.5;n:type:ShaderForge.SFN_ValueProperty,id:6976,x:32034,y:33897,ptovrint:False,ptlb:Falloff,ptin:_Falloff,varname:node_4235,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Power,id:4196,x:32238,y:33775,varname:node_4196,prsc:2|VAL-7952-OUT,EXP-6976-OUT;n:type:ShaderForge.SFN_Lerp,id:6246,x:32424,y:33738,varname:node_6246,prsc:2|A-6964-OUT,B-9878-OUT,T-4196-OUT;n:type:ShaderForge.SFN_Vector3,id:6964,x:32216,y:33529,varname:node_6964,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Vector3,id:9878,x:32216,y:33614,varname:node_9878,prsc:2,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_OneMinus,id:2234,x:33193,y:33885,varname:node_2234,prsc:2|IN-8878-OUT;n:type:ShaderForge.SFN_ComponentMask,id:6545,x:33507,y:33051,varname:node_6545,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-2234-OUT;n:type:ShaderForge.SFN_Add,id:8878,x:32524,y:34057,varname:node_8878,prsc:2|A-6246-OUT,B-9768-OUT;n:type:ShaderForge.SFN_OneMinus,id:9768,x:31881,y:34135,varname:node_9768,prsc:2|IN-4353-RGB;n:type:ShaderForge.SFN_ViewVector,id:2351,x:31139,y:33990,varname:node_2351,prsc:2;n:type:ShaderForge.SFN_Add,id:4780,x:31456,y:33894,varname:node_4780,prsc:2|A-8985-XYZ,B-2351-OUT;n:type:ShaderForge.SFN_Lerp,id:2695,x:32405,y:34469,varname:node_2695,prsc:2|A-8883-OUT,B-6663-OUT,T-1897-OUT;n:type:ShaderForge.SFN_Vector3,id:6663,x:32127,y:34383,varname:node_6663,prsc:2,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_Vector3,id:8883,x:32127,y:34273,varname:node_8883,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Power,id:1897,x:32148,y:34590,varname:node_1897,prsc:2|VAL-3915-OUT,EXP-8172-OUT;n:type:ShaderForge.SFN_Divide,id:3915,x:31964,y:34492,varname:node_3915,prsc:2|A-85-OUT,B-1602-OUT;n:type:ShaderForge.SFN_Distance,id:85,x:31702,y:34517,varname:node_85,prsc:2|A-60-XYZ,B-4780-OUT;n:type:ShaderForge.SFN_OneMinus,id:8172,x:31964,y:34673,varname:node_8172,prsc:2|IN-6976-OUT;n:type:ShaderForge.SFN_Divide,id:1602,x:31754,y:34252,varname:node_1602,prsc:2|A-9049-OUT,B-3134-OUT;n:type:ShaderForge.SFN_Vector1,id:3134,x:31484,y:34340,varname:node_3134,prsc:2,v1:6;n:type:ShaderForge.SFN_Divide,id:3298,x:32854,y:33747,varname:node_3298,prsc:2|A-5156-OUT,B-8878-OUT;n:type:ShaderForge.SFN_OneMinus,id:8717,x:32571,y:33520,varname:node_8717,prsc:2|IN-8038-A;n:type:ShaderForge.SFN_Multiply,id:5156,x:32834,y:33551,varname:node_5156,prsc:2|A-8717-OUT,B-1136-OUT;n:type:ShaderForge.SFN_Vector1,id:1136,x:32633,y:33712,varname:node_1136,prsc:2,v1:30;proporder:851-5927-5328-4865-4353-9049-6976-8038;pass:END;sub:END;*/

Shader "Shader Forge/DistanceGrid" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        _Gloss ("Gloss", Range(0, 1)) = 0.5
        _SpecColor ("Spec Color", Color) = (1,1,1,1)
        _GridAlpha ("GridAlpha", 2D) = "bump" {}
        _Anis ("Anis", Float ) = 4.5
        _Falloff ("Falloff", Float ) = 0
        _GridColor ("GridColor", Color) = (0.5,0.5,0.5,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent+1"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _Color;
            uniform float _Gloss;
            uniform sampler2D _GridAlpha; uniform float4 _GridAlpha_ST;
            uniform float4 _GridColor;
            uniform float _Anis;
            uniform float _Falloff;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
////// Emissive:
                float3 emissive = (_GridColor.rgb*10.0);
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 node_544 = (_Diffuse_var.rgb*_Color.rgb); // Diffuse Color
                float node_7782 = max(0,dot(lightDirection,normalDirection)); // Lambert
                float3 finalColor = emissive + (((node_544*node_7782)+(node_7782*pow(max(0,dot(normalDirection,halfDirection)),exp2(lerp(1,11,_Gloss)))*_SpecColor.rgb))*_LightColor0.rgb*attenuation);
                float3 node_4780 = (_WorldSpaceCameraPos+viewDirection);
                float2 node_2101 = (i.posWorld.rgb.rb*1.0);
                float4 _GridAlpha_var = tex2D(_GridAlpha,TRANSFORM_TEX(node_2101, _GridAlpha));
                float3 node_8878 = (lerp(float3(0,0,0),float3(1,1,1),pow((distance(i.posWorld.rgb,node_4780)/_Anis),_Falloff))+(1.0 - _GridAlpha_var.rgb));
                return fixed4(finalColor,(1.0 - node_8878).r);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _Color;
            uniform float _Gloss;
            uniform sampler2D _GridAlpha; uniform float4 _GridAlpha_ST;
            uniform float4 _GridColor;
            uniform float _Anis;
            uniform float _Falloff;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 node_544 = (_Diffuse_var.rgb*_Color.rgb); // Diffuse Color
                float node_7782 = max(0,dot(lightDirection,normalDirection)); // Lambert
                float3 finalColor = (((node_544*node_7782)+(node_7782*pow(max(0,dot(normalDirection,halfDirection)),exp2(lerp(1,11,_Gloss)))*_SpecColor.rgb))*_LightColor0.rgb*attenuation);
                float3 node_4780 = (_WorldSpaceCameraPos+viewDirection);
                float2 node_2101 = (i.posWorld.rgb.rb*1.0);
                float4 _GridAlpha_var = tex2D(_GridAlpha,TRANSFORM_TEX(node_2101, _GridAlpha));
                float3 node_8878 = (lerp(float3(0,0,0),float3(1,1,1),pow((distance(i.posWorld.rgb,node_4780)/_Anis),_Falloff))+(1.0 - _GridAlpha_var.rgb));
                return fixed4(finalColor * (1.0 - node_8878).r,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
