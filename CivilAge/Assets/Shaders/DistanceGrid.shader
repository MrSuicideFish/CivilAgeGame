// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:Legacy Shaders/Transparent/Diffuse,iptp:0,cusa:False,bamd:2,lico:0,lgpr:1,limd:2,spmd:1,trmd:0,grmd:0,uamb:False,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:6,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:34242,y:32524,varname:node_4013,prsc:2|diff-8837-OUT,emission-8837-OUT,alpha-2325-OUT;n:type:ShaderForge.SFN_Tex2d,id:9773,x:31203,y:33154,ptovrint:False,ptlb:GridAlpha,ptin:_GridAlpha,varname:node_9773,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:529239097d02f9f42b0ddd436c6fcbb0,ntxv:3,isnm:False|UVIN-608-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:4358,x:31203,y:32696,varname:node_4358,prsc:2;n:type:ShaderForge.SFN_ComponentMask,id:6781,x:31203,y:32844,varname:node_6781,prsc:2,cc1:0,cc2:2,cc3:-1,cc4:-1|IN-4358-XYZ;n:type:ShaderForge.SFN_Multiply,id:608,x:31203,y:33001,varname:node_608,prsc:2|A-6781-OUT,B-3999-OUT;n:type:ShaderForge.SFN_Vector1,id:3999,x:30976,y:33016,varname:node_3999,prsc:2,v1:1;n:type:ShaderForge.SFN_Color,id:2823,x:32693,y:32335,ptovrint:False,ptlb:GridColor,ptin:_GridColor,varname:node_2823,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:8837,x:32952,y:32318,varname:node_8837,prsc:2|A-2823-RGB,B-765-OUT;n:type:ShaderForge.SFN_Vector1,id:765,x:32791,y:32484,varname:node_765,prsc:2,v1:10;n:type:ShaderForge.SFN_Distance,id:2130,x:32686,y:32724,varname:node_2130,prsc:2|A-4358-XYZ,B-2574-OUT;n:type:ShaderForge.SFN_ViewPosition,id:6155,x:32034,y:32791,varname:node_6155,prsc:2;n:type:ShaderForge.SFN_Divide,id:7683,x:32929,y:32724,varname:node_7683,prsc:2|A-2130-OUT,B-7719-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7719,x:32700,y:32879,ptovrint:False,ptlb:Anis,ptin:_Anis,varname:node_7719,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4.5;n:type:ShaderForge.SFN_ValueProperty,id:4235,x:32929,y:32892,ptovrint:False,ptlb:Falloff,ptin:_Falloff,varname:node_4235,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Power,id:2290,x:33133,y:32751,varname:node_2290,prsc:2|VAL-7683-OUT,EXP-4235-OUT;n:type:ShaderForge.SFN_Lerp,id:8346,x:33324,y:32707,varname:node_8346,prsc:2|A-1336-OUT,B-3224-OUT,T-2290-OUT;n:type:ShaderForge.SFN_Vector3,id:1336,x:33111,y:32505,varname:node_1336,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Vector3,id:3224,x:33111,y:32590,varname:node_3224,prsc:2,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_OneMinus,id:213,x:33694,y:32889,varname:node_213,prsc:2|IN-9698-OUT;n:type:ShaderForge.SFN_ComponentMask,id:2325,x:33859,y:32986,varname:node_2325,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-213-OUT;n:type:ShaderForge.SFN_Add,id:9698,x:33504,y:32985,varname:node_9698,prsc:2|A-8346-OUT,B-9843-OUT;n:type:ShaderForge.SFN_OneMinus,id:9843,x:32775,y:33111,varname:node_9843,prsc:2|IN-9773-RGB;n:type:ShaderForge.SFN_ViewVector,id:4637,x:32034,y:32980,varname:node_4637,prsc:2;n:type:ShaderForge.SFN_Vector1,id:1734,x:32360,y:33092,varname:node_1734,prsc:2,v1:1.6;n:type:ShaderForge.SFN_Add,id:2574,x:32351,y:32870,varname:node_2574,prsc:2|A-6155-XYZ,B-4637-OUT;proporder:9773-2823-7719-4235;pass:END;sub:END;*/

Shader "Shader Forge/DistanceGrid" {
    Properties {
        _GridAlpha ("GridAlpha", 2D) = "bump" {}
        _GridColor ("GridColor", Color) = (0.5,0.5,0.5,1)
        _Anis ("Anis", Float ) = 4.5
        _Falloff ("Falloff", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "DisableBatching"="LODFading"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On
            AlphaToMask On
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _GridAlpha; uniform float4 _GridAlpha_ST;
            uniform float4 _GridColor;
            uniform float _Anis;
            uniform float _Falloff;
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
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 node_8837 = (_GridColor.rgb*10.0);
                float3 diffuseColor = node_8837;
                float3 diffuse = directDiffuse * diffuseColor;
////// Emissive:
                float3 emissive = node_8837;
/// Final Color:
                float3 finalColor = diffuse + emissive;
                float2 node_608 = (i.posWorld.rgb.rb*1.0);
                float4 _GridAlpha_var = tex2D(_GridAlpha,TRANSFORM_TEX(node_608, _GridAlpha));
                return fixed4(finalColor,(1.0 - (lerp(float3(0,0,0),float3(1,1,1),pow((distance(i.posWorld.rgb,(_WorldSpaceCameraPos+viewDirection))/_Anis),_Falloff))+(1.0 - _GridAlpha_var.rgb))).r);
            }
            ENDCG
        }
    }
    FallBack "Legacy Shaders/Transparent/Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
