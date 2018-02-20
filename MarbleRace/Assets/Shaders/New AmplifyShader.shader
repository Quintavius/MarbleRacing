// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Terrain/Vaporwave"
{
	Properties
	{
		_Grid("Grid", 2D) = "white" {}
		[HDR]_GridColor("GridColor", Color) = (0,0,0,0)
		_GradientPower("GradientPower", Float) = 0
		_Exponent("Exponent", Float) = 0
		_Frequency("Frequency", Float) = 0
		_Speed("Speed", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform sampler2D _Grid;
		uniform float4 _Grid_ST;
		uniform float4 _GridColor;
		uniform float _GradientPower;
		uniform float _Exponent;
		uniform float _Speed;
		uniform float _Frequency;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Grid = i.uv_texcoord * _Grid_ST.xy + _Grid_ST.zw;
			float4 tex2DNode7 = tex2D( _Grid, uv_Grid );
			o.Albedo = tex2DNode7.rgb;
			float3 ase_worldPos = i.worldPos;
			float lerpResult44 = lerp( ase_worldPos.x , ase_worldPos.z , 0.4);
			float mulTime40 = _Time.y * _Speed;
			float temp_output_19_0 = abs( pow( _Exponent , sin( ( ( lerpResult44 + mulTime40 ) * _Frequency ) ) ) );
			o.Emission = ( ( tex2DNode7.r * _GridColor ) * ( _GradientPower / temp_output_19_0 ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14301
873;256;1504;908;3328.302;169.2784;1.719065;True;False
Node;AmplifyShaderEditor.RangedFloatNode;41;-2432.857,324.2838;Float;False;Property;_Speed;Speed;6;0;Create;True;0;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;13;-2715.594,666.1088;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldPosInputsNode;10;-2713.579,455.256;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;44;-2479.083,559.6052;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.4;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;40;-2256.857,222.0839;Float;True;1;0;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;35;-2034.87,268.9235;Float;True;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-2121.844,701.3028;Float;False;Property;_Frequency;Frequency;5;0;Create;True;0;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-1893.627,568.4156;Float;True;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-1978.094,823.9177;Float;False;Property;_Exponent;Exponent;4;0;Create;True;0;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;30;-1678.369,566.9578;Float;True;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;33;-1461.622,653.3072;Float;True;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;9;-1351.19,286.2529;Float;False;Property;_GridColor;GridColor;2;1;[HDR];Create;True;0,0,0,0;2.64223,0.2644034,5.137,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;14;-1031.632,561.7153;Float;False;Property;_GradientPower;GradientPower;3;0;Create;True;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;7;-1468.673,70.33915;Float;True;Property;_Grid;Grid;1;0;Create;True;None;5f3bce320adaae743b520dd0f9c671a2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.AbsOpNode;19;-1186.625,647.529;Float;True;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;16;-785.9451,478.1481;Float;True;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-846.9827,324.2809;Float;False;2;2;0;FLOAT;0.0;False;1;COLOR;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-784.5526,19.13119;Float;False;Property;_Float1;Float 1;0;0;Create;True;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-1068.999,-102.17;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-500.313,424.5939;Float;True;2;2;0;COLOR;0.0;False;1;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;6;-563.6401,-34.1715;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;4;-814.0936,-107.2323;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;1;-1430.812,-234.3369;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;2;-1214.258,-40.71429;Float;False;Constant;_Float0;Float 0;0;0;Create;True;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;26;-1780.144,1201.866;Float;False;1;0;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-815.4527,750.9476;Float;True;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;20;-1000.776,865.3362;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Terrain/Vaporwave;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;44;0;10;1
WireConnection;44;1;13;3
WireConnection;40;0;41;0
WireConnection;35;0;44;0
WireConnection;35;1;40;0
WireConnection;31;0;35;0
WireConnection;31;1;32;0
WireConnection;30;0;31;0
WireConnection;33;0;34;0
WireConnection;33;1;30;0
WireConnection;19;0;33;0
WireConnection;16;0;14;0
WireConnection;16;1;19;0
WireConnection;8;0;7;1
WireConnection;8;1;9;0
WireConnection;3;0;1;2
WireConnection;3;1;2;0
WireConnection;11;0;8;0
WireConnection;11;1;16;0
WireConnection;6;0;4;0
WireConnection;6;1;5;0
WireConnection;4;0;3;0
WireConnection;12;0;19;0
WireConnection;12;1;20;0
WireConnection;20;0;13;3
WireConnection;0;0;7;0
WireConnection;0;2;11;0
WireConnection;0;10;7;4
ASEEND*/
//CHKSM=B72A9D16BBA83803F1426C3536FDF0C6192C06ED