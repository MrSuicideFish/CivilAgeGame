// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|emission-246-OUT,custl-5085-OUT,alpha-2086-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:8068,x:32734,y:33086,varname:node_8068,prsc:2;n:type:ShaderForge.SFN_LightColor,id:3406,x:32734,y:32952,varname:node_3406,prsc:2;n:type:ShaderForge.SFN_LightVector,id:6869,x:31858,y:32654,varname:node_6869,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:9684,x:31858,y:32782,prsc:2,pt:True;n:type:ShaderForge.SFN_HalfVector,id:9471,x:31858,y:32933,varname:node_9471,prsc:2;n:type:ShaderForge.SFN_Dot,id:7782,x:32070,y:32697,cmnt:Lambert,varname:node_7782,prsc:2,dt:1|A-6869-OUT,B-9684-OUT;n:type:ShaderForge.SFN_Dot,id:3269,x:32070,y:32871,cmnt:Blinn-Phong,varname:node_3269,prsc:2,dt:1|A-9684-OUT,B-9471-OUT;n:type:ShaderForge.SFN_Multiply,id:2746,x:32465,y:32866,cmnt:Specular Contribution,varname:node_2746,prsc:2|A-7782-OUT,B-5267-OUT,C-4865-RGB;n:type:ShaderForge.SFN_Tex2d,id:851,x:32070,y:32349,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_851,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1941,x:32465,y:32693,cmnt:Diffuse Contribution,varname:node_1941,prsc:2|A-544-OUT,B-7782-OUT;n:type:ShaderForge.SFN_Color,id:5927,x:32070,y:32534,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_5927,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Exp,id:1700,x:32070,y:33054,varname:node_1700,prsc:2,et:1|IN-9978-OUT;n:type:ShaderForge.SFN_Slider,id:5328,x:31529,y:33056,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_5328,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Power,id:5267,x:32268,y:32940,varname:node_5267,prsc:2|VAL-3269-OUT,EXP-1700-OUT;n:type:ShaderForge.SFN_Add,id:2159,x:32734,y:32812,cmnt:Combine,varname:node_2159,prsc:2|A-1941-OUT,B-2746-OUT;n:type:ShaderForge.SFN_Multiply,id:5085,x:32979,y:32952,cmnt:Attenuate and Color,varname:node_5085,prsc:2|A-2159-OUT,B-3406-RGB,C-8068-OUT;n:type:ShaderForge.SFN_ConstantLerp,id:9978,x:31858,y:33056,varname:node_9978,prsc:2,a:1,b:11|IN-5328-OUT;n:type:ShaderForge.SFN_Color,id:4865,x:32268,y:33095,ptovrint:False,ptlb:Spec Color,ptin:_SpecColor,varname:node_4865,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_AmbientLight,id:7528,x:32734,y:32646,varname:node_7528,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2460,x:32927,y:32598,cmnt:Ambient Light,varname:node_2460,prsc:2|A-544-OUT,B-7528-RGB;n:type:ShaderForge.SFN_Multiply,id:544,x:32268,y:32448,cmnt:Diffuse Color,varname:node_544,prsc:2|A-851-RGB,B-5927-RGB;n:type:ShaderForge.SFN_ComponentMask,id:2086,x:32945,y:33242,varname:node_2086,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-8892-OUT;n:type:ShaderForge.SFN_Tex2d,id:1834,x:29746,y:34322,ptovrint:False,ptlb:GridAlpha,ptin:_GridAlpha,varname:node_9773,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:529239097d02f9f42b0ddd436c6fcbb0,ntxv:3,isnm:False|UVIN-5713-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:2515,x:29746,y:33911,varname:node_2515,prsc:2;n:type:ShaderForge.SFN_ComponentMask,id:1158,x:29746,y:34035,varname:node_1158,prsc:2,cc1:0,cc2:2,cc3:-1,cc4:-1|IN-2515-XYZ;n:type:ShaderForge.SFN_Multiply,id:5713,x:29746,y:34178,varname:node_5713,prsc:2|A-1158-OUT,B-2886-OUT;n:type:ShaderForge.SFN_Vector1,id:2886,x:29579,y:34198,varname:node_2886,prsc:2,v1:1;n:type:ShaderForge.SFN_Color,id:8510,x:31236,y:33550,ptovrint:False,ptlb:GridColor,ptin:_GridColor,varname:node_2823,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:246,x:31495,y:33533,varname:node_246,prsc:2|A-8510-RGB,B-6542-OUT;n:type:ShaderForge.SFN_Vector1,id:6542,x:31334,y:33699,varname:node_6542,prsc:2,v1:10;n:type:ShaderForge.SFN_Distance,id:6145,x:31229,y:33939,varname:node_6145,prsc:2|A-2515-XYZ,B-185-OUT;n:type:ShaderForge.SFN_ViewPosition,id:4086,x:30577,y:34006,varname:node_4086,prsc:2;n:type:ShaderForge.SFN_Divide,id:3579,x:31472,y:33939,varname:node_3579,prsc:2|A-6145-OUT,B-9436-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9436,x:31229,y:34088,ptovrint:False,ptlb:Anis,ptin:_Anis,varname:node_7719,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4.5;n:type:ShaderForge.SFN_ValueProperty,id:9106,x:31472,y:34088,ptovrint:False,ptlb:Falloff,ptin:_Falloff,varname:node_4235,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Power,id:7896,x:31676,y:33966,varname:node_7896,prsc:2|VAL-3579-OUT,EXP-9106-OUT;n:type:ShaderForge.SFN_Lerp,id:1998,x:31862,y:33929,varname:node_1998,prsc:2|A-4220-OUT,B-5938-OUT,T-7896-OUT;n:type:ShaderForge.SFN_Vector3,id:4220,x:31654,y:33720,varname:node_4220,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Vector3,id:5938,x:31654,y:33805,varname:node_5938,prsc:2,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_OneMinus,id:8892,x:32631,y:34076,varname:node_8892,prsc:2|IN-4822-OUT;n:type:ShaderForge.SFN_Add,id:4822,x:31962,y:34248,varname:node_4822,prsc:2|A-1998-OUT,B-5810-OUT;n:type:ShaderForge.SFN_OneMinus,id:5810,x:31319,y:34326,varname:node_5810,prsc:2|IN-1834-RGB;n:type:ShaderForge.SFN_ViewVector,id:3,x:30577,y:34181,varname:node_3,prsc:2;n:type:ShaderForge.SFN_Add,id:185,x:30894,y:34085,varname:node_185,prsc:2|A-4086-XYZ,B-3-OUT;n:type:ShaderForge.SFN_Lerp,id:3122,x:31843,y:34660,varname:node_3122,prsc:2|A-5817-OUT,B-1670-OUT,T-3080-OUT;n:type:ShaderForge.SFN_Vector3,id:1670,x:31565,y:34574,varname:node_1670,prsc:2,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_Vector3,id:5817,x:31565,y:34464,varname:node_5817,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Power,id:3080,x:31586,y:34781,varname:node_3080,prsc:2|VAL-5045-OUT,EXP-3534-OUT;n:type:ShaderForge.SFN_Divide,id:5045,x:31402,y:34683,varname:node_5045,prsc:2|A-9952-OUT,B-4907-OUT;n:type:ShaderForge.SFN_Distance,id:9952,x:31140,y:34708,varname:node_9952,prsc:2|A-2515-XYZ,B-185-OUT;n:type:ShaderForge.SFN_OneMinus,id:3534,x:31402,y:34864,varname:node_3534,prsc:2|IN-9106-OUT;n:type:ShaderForge.SFN_Divide,id:4907,x:31192,y:34443,varname:node_4907,prsc:2|A-9436-OUT,B-9341-OUT;n:type:ShaderForge.SFN_Vector1,id:9341,x:30922,y:34531,varname:node_9341,prsc:2,v1:6;n:type:ShaderForge.SFN_Divide,id:1464,x:32292,y:33938,varname:node_1464,prsc:2|A-8600-OUT,B-4822-OUT;n:type:ShaderForge.SFN_OneMinus,id:2177,x:32009,y:33711,varname:node_2177,prsc:2|IN-8510-A;n:type:ShaderForge.SFN_Multiply,id:8600,x:32272,y:33742,varname:node_8600,prsc:2|A-2177-OUT,B-8254-OUT;n:type:ShaderForge.SFN_Vector1,id:8254,x:32071,y:33903,varname:node_8254,prsc:2,v1:30;proporder:851-5927-5328-4865-1834-9436-9106-8510;pass:END;sub:END;*/

Shader "Shader Forge/FresnelGrid" {
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
            "Queue"="Transparent"
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
            #pragma multi_compile_fog
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
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
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
                float3 node_185 = (_WorldSpaceCameraPos+viewDirection);
                float2 node_5713 = (i.posWorld.rgb.rb*1.0);
                float4 _GridAlpha_var = tex2D(_GridAlpha,TRANSFORM_TEX(node_5713, _GridAlpha));
                float3 node_4822 = (lerp(float3(0,0,0),float3(1,1,1),pow((distance(i.posWorld.rgb,node_185)/_Anis),_Falloff))+(1.0 - _GridAlpha_var.rgb));
                fixed4 finalRGBA = fixed4(finalColor,(1.0 - node_4822).r);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
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
            #pragma multi_compile_fog
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
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
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
                float3 node_185 = (_WorldSpaceCameraPos+viewDirection);
                float2 node_5713 = (i.posWorld.rgb.rb*1.0);
                float4 _GridAlpha_var = tex2D(_GridAlpha,TRANSFORM_TEX(node_5713, _GridAlpha));
                float3 node_4822 = (lerp(float3(0,0,0),float3(1,1,1),pow((distance(i.posWorld.rgb,node_185)/_Anis),_Falloff))+(1.0 - _GridAlpha_var.rgb));
                fixed4 finalRGBA = fixed4(finalColor * (1.0 - node_4822).r,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
