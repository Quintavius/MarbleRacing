// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Marbles/Ice"
{
	Properties
	{
		[Header(Translucency)]
		_Translucency("Strength", Range( 0 , 50)) = 1
		_TransNormalDistortion("Normal Distortion", Range( 0 , 1)) = 0.1
		_TransScattering("Scaterring Falloff", Range( 1 , 50)) = 2
		_TransDirect("Direct", Range( 0 , 1)) = 1
		_TransAmbient("Ambient", Range( 0 , 1)) = 0.2
		_TransShadow("Shadow", Range( 0 , 1)) = 0.9
		_HeightMultiplier("HeightMultiplier", Range( -2 , 2)) = 0
		_BaseTexture("BaseTexture", 2D) = "white" {}
		_HeightScale("HeightScale", Range( -2 , 2)) = 0
		_Normal("Normal", 2D) = "bump" {}
		_NoiseTexture("NoiseTexture", 2D) = "white" {}
		_SurfaceColor("SurfaceColor", Color) = (0.5588235,0.5588235,0.5588235,0)
		_DeepColor("DeepColor", Color) = (1,1,1,0)
		_Scratches("Scratches", 2D) = "white" {}
		_Smoothness("Smoothness", Range( 0 , 1)) = 0.811698
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#pragma target 3.0
		#pragma surface surf StandardCustom keepalpha addshadow fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		struct SurfaceOutputStandardCustom
		{
			fixed3 Albedo;
			fixed3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			fixed Alpha;
			fixed3 Transmission;
			fixed3 Translucency;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Scratches;
		uniform float4 _Scratches_ST;
		uniform float4 _DeepColor;
		uniform float4 _SurfaceColor;
		uniform sampler2D _NoiseTexture;
		uniform float _HeightMultiplier;
		uniform sampler2D _BaseTexture;
		uniform float4 _BaseTexture_ST;
		uniform float _HeightScale;
		uniform float _Smoothness;
		uniform half _Translucency;
		uniform half _TransNormalDistortion;
		uniform half _TransScattering;
		uniform half _TransDirect;
		uniform half _TransAmbient;
		uniform half _TransShadow;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float2 uv_BaseTexture = v.texcoord * _BaseTexture_ST.xy + _BaseTexture_ST.zw;
			float4 tex2DNode3 = tex2Dlod( _BaseTexture, float4( uv_BaseTexture, 0, 0.0) );
			float3 ase_vertexNormal = v.normal.xyz;
			float2 uv_TexCoord28 = v.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
			float2 temp_output_29_0 = frac( uv_TexCoord28 );
			float2 smoothstepResult30 = smoothstep( float2( 0,0 ) , float2( 0.1,0.1 ) , temp_output_29_0);
			float2 smoothstepResult34 = smoothstep( float2( 1,1 ) , float2( 0.9,0.9 ) , temp_output_29_0);
			v.vertex.xyz += ( ( ( ( tex2DNode3.g - 0.5 ) * ase_vertexNormal ) * 0.0 ) * ( ( smoothstepResult30 * smoothstepResult34 ).x * ( smoothstepResult30 * smoothstepResult34 ).y ) );
		}

		inline half4 LightingStandardCustom(SurfaceOutputStandardCustom s, half3 viewDir, UnityGI gi )
		{
			#if !DIRECTIONAL
			float3 lightAtten = gi.light.color;
			#else
			float3 lightAtten = lerp( _LightColor0.rgb, gi.light.color, _TransShadow );
			#endif
			half3 lightDir = gi.light.dir + s.Normal * _TransNormalDistortion;
			half transVdotL = pow( saturate( dot( viewDir, -lightDir ) ), _TransScattering );
			half3 translucency = lightAtten * (transVdotL * _TransDirect + gi.indirect.diffuse * _TransAmbient) * s.Translucency;
			half4 c = half4( s.Albedo * translucency * _Translucency, 0 );

			half3 transmission = max(0 , -dot(s.Normal, gi.light.dir)) * gi.light.color * s.Transmission;
			half4 d = half4(s.Albedo * transmission , 0);

			SurfaceOutputStandard r;
			r.Albedo = s.Albedo;
			r.Normal = s.Normal;
			r.Emission = s.Emission;
			r.Metallic = s.Metallic;
			r.Smoothness = s.Smoothness;
			r.Occlusion = s.Occlusion;
			r.Alpha = s.Alpha;
			return LightingStandard (r, viewDir, gi) + c + d;
		}

		inline void LightingStandardCustom_GI(SurfaceOutputStandardCustom s, UnityGIInput data, inout UnityGI gi )
		{
			#if defined(UNITY_PASS_DEFERRED) && UNITY_ENABLE_REFLECTION_BUFFERS
				gi = UnityGlobalIllumination(data, s.Occlusion, s.Normal);
			#else
				UNITY_GLOSSY_ENV_FROM_SURFACE( g, s, data );
				gi = UnityGlobalIllumination( data, s.Occlusion, s.Normal, g );
			#endif
		}

		void surf( Input i , inout SurfaceOutputStandardCustom o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float3 tex2DNode10 = UnpackScaleNormal( tex2D( _Normal, uv_Normal ) ,0.0 );
			o.Normal = tex2DNode10;
			float2 uv_Scratches = i.uv_texcoord * _Scratches_ST.xy + _Scratches_ST.zw;
			float2 uv_TexCoord6 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float2 uv_BaseTexture = i.uv_texcoord * _BaseTexture_ST.xy + _BaseTexture_ST.zw;
			float4 tex2DNode3 = tex2D( _BaseTexture, uv_BaseTexture );
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float2 appendResult13 = (float2(ase_worldViewDir.x , ase_worldViewDir.y));
			float2 appendResult11 = (float2(tex2DNode10.r , tex2DNode10.g));
			float3 appendResult15 = (float3(( appendResult13 + appendResult11 ) , ase_worldViewDir.z));
			float3 normalizeResult16 = normalize( appendResult15 );
			float2 paralaxOffset4 = ParallaxOffset( ( _HeightMultiplier * tex2DNode3.g ) , _HeightScale , normalizeResult16 );
			float4 lerpResult18 = lerp( _DeepColor , _SurfaceColor , tex2D( _NoiseTexture, ( uv_TexCoord6 + paralaxOffset4 ) ).g);
			float4 temp_output_25_0 = saturate( ( tex2D( _Scratches, uv_Scratches ) + lerpResult18 ) );
			o.Albedo = temp_output_25_0.rgb;
			o.Smoothness = _Smoothness;
			o.Transmission = temp_output_25_0.rgb;
			o.Translucency = temp_output_25_0.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14301
219;1000;1504;908;3662.652;846.637;1.904178;True;False
Node;AmplifyShaderEditor.RangedFloatNode;8;-3040.89,563.3348;Float;False;Constant;_NormalScale;NormalScale;2;0;Create;True;0;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;12;-2710.325,344.8601;Float;False;World;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;10;-2714.935,510.8825;Float;True;Property;_Normal;Normal;9;0;Create;True;77fdad851e93f394c9f8a1b1a63b56f3;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;13;-2403.211,361.6557;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;11;-2379.215,546.405;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;14;-2182.47,409.6426;Float;False;2;2;0;FLOAT2;0.0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1;-2207.406,-107.046;Float;False;Property;_HeightMultiplier;HeightMultiplier;6;0;Create;True;0;0;-2;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;15;-2038.506,448.0316;Float;False;FLOAT3;4;0;FLOAT2;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;3;-2260.717,66.36382;Float;True;Property;_BaseTexture;BaseTexture;7;0;Create;True;d01457b88b1c5174ea4235d140b5fab8;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;5;-2165.225,289.1776;Float;False;Property;_HeightScale;HeightScale;8;0;Create;True;0;0;-2;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;28;-3090.687,822.7504;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalizeNode;16;-1868.155,438.4343;Float;False;1;0;FLOAT3;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-1766.718,-53.6362;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;29;-2754.15,812.8101;Float;False;1;0;FLOAT2;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;37;-2901.746,1162.113;Float;False;Constant;_RightDownBorder;RightDownBorder;10;0;Create;True;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;38;-2891.846,1315.591;Float;False;Constant;_RightDownLimit;RightDownLimit;10;0;Create;True;0.9,0.9;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;36;-3102.258,1095.275;Float;False;Constant;_LeftUpLimit;LeftUpLimit;10;0;Create;True;0.1,0.1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-1558.423,-87.52616;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ParallaxOffsetHlpNode;4;-1481.427,75.97737;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;35;-3097.311,964.0754;Float;False;Constant;_LeftUpBorder;LeftUpBorder;10;0;Create;True;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SmoothstepOpNode;30;-2570.278,798.0999;Float;False;3;0;FLOAT2;0.0;False;1;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SmoothstepOpNode;34;-2567.565,993.781;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-1739.448,646.8584;Float;False;Constant;_AvgHeight;AvgHeight;10;0;Create;True;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;7;-1248.208,18.01581;Float;False;2;2;0;FLOAT2;0.0,0;False;1;FLOAT2;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;21;-812.0896,-300.8307;Float;False;Property;_SurfaceColor;SurfaceColor;11;0;Create;True;0.5588235,0.5588235,0.5588235,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;22;-791.9711,-488.6039;Float;False;Property;_DeepColor;DeepColor;12;0;Create;True;1,1,1,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-2347.181,876.5518;Float;False;2;2;0;FLOAT2;0.0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NormalVertexDataNode;42;-1554.874,764.8555;Float;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;17;-935.1451,-36.38915;Float;True;Property;_NoiseTexture;NoiseTexture;10;0;Create;True;d01457b88b1c5174ea4235d140b5fab8;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;40;-1525.64,595.4244;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;18;-439.853,-163.1389;Float;False;3;0;COLOR;0.0,0,0,0;False;1;COLOR;0.0;False;2;FLOAT;0.0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-1301.076,819.653;Float;False;Constant;_DisplaceIntensity;DisplaceIntensity;10;0;Create;True;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;23;-479.5269,-407.3872;Float;True;Property;_Scratches;Scratches;13;0;Create;True;6e6cba53deb4f4e41a81667b73a1ca42;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;32;-2114.277,847.1324;Float;False;FLOAT2;1;0;FLOAT2;0.0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-1315.496,655.2613;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT3;0.0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-1811.158,893.3561;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-998.2502,698.522;Float;False;2;2;0;FLOAT3;0.0;False;1;FLOAT;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;24;-72.06913,-137.5836;Float;False;2;2;0;COLOR;0.0;False;1;COLOR;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;27;7.770235,324.9363;Float;False;Property;_Smoothness;Smoothness;14;0;Create;True;0.811698;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-828.1974,847.8264;Float;False;2;2;0;FLOAT3;0.0;False;1;FLOAT;0.0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SaturateNode;25;60.07913,-115.5589;Float;False;1;0;COLOR;0.0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;449.7899,183.0383;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Marbles/Ice;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;0;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;5;8;0
WireConnection;13;0;12;1
WireConnection;13;1;12;2
WireConnection;11;0;10;1
WireConnection;11;1;10;2
WireConnection;14;0;13;0
WireConnection;14;1;11;0
WireConnection;15;0;14;0
WireConnection;15;2;12;3
WireConnection;16;0;15;0
WireConnection;2;0;1;0
WireConnection;2;1;3;2
WireConnection;29;0;28;0
WireConnection;4;0;2;0
WireConnection;4;1;5;0
WireConnection;4;2;16;0
WireConnection;30;0;29;0
WireConnection;30;1;35;0
WireConnection;30;2;36;0
WireConnection;34;0;29;0
WireConnection;34;1;37;0
WireConnection;34;2;38;0
WireConnection;7;0;6;0
WireConnection;7;1;4;0
WireConnection;31;0;30;0
WireConnection;31;1;34;0
WireConnection;17;1;7;0
WireConnection;40;0;3;2
WireConnection;40;1;39;0
WireConnection;18;0;22;0
WireConnection;18;1;21;0
WireConnection;18;2;17;2
WireConnection;32;0;31;0
WireConnection;41;0;40;0
WireConnection;41;1;42;0
WireConnection;33;0;32;0
WireConnection;33;1;32;1
WireConnection;43;0;41;0
WireConnection;43;1;44;0
WireConnection;24;0;23;0
WireConnection;24;1;18;0
WireConnection;45;0;43;0
WireConnection;45;1;33;0
WireConnection;25;0;24;0
WireConnection;0;0;25;0
WireConnection;0;1;10;0
WireConnection;0;4;27;0
WireConnection;0;6;25;0
WireConnection;0;7;25;0
WireConnection;0;11;45;0
ASEEND*/
//CHKSM=8EC6FE4F27221E8202DE7AD825A05964D2C298BC