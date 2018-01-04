// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "PlayerIndicator"
{
	Properties
	{
		_PlayerColor("PlayerColor", Color) = (0,0,0,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Overlay+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		ZTest Always
		Stencil
		{
			Ref 255
			Comp NotEqual
			Pass Zero
			Fail Keep
		}
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow 
		struct Input
		{
			fixed filler;
		};

		uniform float4 _PlayerColor;

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Emission = _PlayerColor.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14201
889;260;968;572;1208.339;441.893;1.154483;True;True
Node;AmplifyShaderEditor.ColorNode;1;-422,-61;Float;False;Property;_PlayerColor;PlayerColor;1;0;Create;0,0,0,0;1,0.7931035,0.2499999,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;PlayerIndicator;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;Back;0;7;False;-5000;0;Custom;0.5;True;False;0;True;Opaque;Overlay;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;255;255;255;6;2;1;0;8;2;2;2;False;2;15;10;25;False;0.5;False;0;One;OneMinusSrcAlpha;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;2;1;0
ASEEND*/
//CHKSM=7DA81CC13927A4EB0F3014860F6B16F331136723