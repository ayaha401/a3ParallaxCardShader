using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardShaderGUI : ShaderGUI
{
    MaterialProperty StackColor;

    MaterialProperty ParallaxScale;
    
    MaterialProperty Layer0;
    MaterialProperty Layer0Height;
    MaterialProperty Layer0StackColor;

    MaterialProperty UseLayer1;
    MaterialProperty Layer1;
    MaterialProperty Layer1Height;
    MaterialProperty Layer1StackColor;

    MaterialProperty UseLayer2;
    MaterialProperty Layer2;
    MaterialProperty Layer2Height;
    MaterialProperty Layer2StackColor;

    MaterialProperty UseLayer3;
    MaterialProperty Layer3;
    MaterialProperty Layer3Height;
    MaterialProperty Layer3StackColor;

    MaterialProperty UseLayer4;
    MaterialProperty Layer4;
    MaterialProperty Layer4Height;
    MaterialProperty Layer4StackColor;

    MaterialProperty BackTex;
    MaterialProperty BackTexStackColor;

    MaterialProperty DissolveTex;
    MaterialProperty DissolveAmount;

    MaterialProperty UseEdge;
    MaterialProperty EdgeColor;
    MaterialProperty EdgeRange;
    MaterialProperty EdgeBlur;

    Texture a;
    
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] Prop)
    {
        var material = (Material)materialEditor.target;

        StackColor = FindProperty("_StackColor", Prop, false);

        ParallaxScale = FindProperty("_ParallaxScale", Prop, false);

        Layer0 = FindProperty("_Layer0", Prop, false);
        Layer0Height = FindProperty("_Layer0Height", Prop, false);
        Layer0StackColor = FindProperty("_Layer0StackColor", Prop, false);

        UseLayer1 = FindProperty("_UseLayer1", Prop, false);
        Layer1 = FindProperty("_Layer1", Prop, false);
        Layer1Height = FindProperty("_Layer1Height", Prop, false);
        Layer1StackColor = FindProperty("_Layer1StackColor", Prop, false);

        UseLayer2 = FindProperty("_UseLayer2", Prop, false);
        Layer2 = FindProperty("_Layer2", Prop, false);
        Layer2Height = FindProperty("_Layer2Height", Prop, false);
        Layer2StackColor = FindProperty("_Layer2StackColor", Prop, false);

        UseLayer3 = FindProperty("_UseLayer3", Prop, false);
        Layer3 = FindProperty("_Layer3", Prop, false);
        Layer3Height = FindProperty("_Layer3Height", Prop, false);
        Layer3StackColor = FindProperty("_Layer3StackColor", Prop, false);

        UseLayer4 = FindProperty("_UseLayer4", Prop, false);
        Layer4 = FindProperty("_Layer4", Prop, false);
        Layer4Height = FindProperty("_Layer4Height", Prop, false);
        Layer4StackColor = FindProperty("_Layer4StackColor", Prop, false);

        BackTex = FindProperty("_BackTex", Prop, false);
        BackTexStackColor = FindProperty("_BackTexStackColor", Prop, false);

        DissolveTex = FindProperty("_DissolveTex", Prop, false);
        DissolveAmount = FindProperty("_DissolveAmount", Prop, false);

        UseEdge = FindProperty("_UseEdge", Prop, false);
        EdgeColor = FindProperty("_EdgeColor", Prop, false);
        EdgeRange = FindProperty("_EdgeRange", Prop, false);
        EdgeBlur = FindProperty("_EdgeBlur", Prop, false);



        // base.OnGUI (materialEditor, Prop);

        GUILayout.Label("Information", EditorStyles.boldLabel);
        using (new EditorGUILayout.VerticalScope("box"))
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Label("Version");
                GUILayout.Label("Version 1.0.0");
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Label("How to use (Japanese)");
                if(GUILayout.Button("How to use (Japanese)"))
                {
                    System.Diagnostics.Process.Start("");
                }
            }
        }
        
        
        // ================================================================================================
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        // ================================================================================================

        GUILayout.Label("Parallax", EditorStyles.boldLabel);
        using (new EditorGUILayout.VerticalScope("box"))
        {
            using (new EditorGUILayout.HorizontalScope("box"))
            {
                GUILayout.Label("ParallaxScale");
                ParallaxScale.floatValue = EditorGUILayout.Slider(ParallaxScale.floatValue, ParallaxScale.rangeLimits.x, ParallaxScale.rangeLimits.y);
                GUILayout.Space(64.0f);
            }
        }

        // ================================================================================================
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        // ================================================================================================

        GUILayout.Label("StackColor", EditorStyles.boldLabel);
        using (new EditorGUILayout.VerticalScope("box"))
        {   
            using (new EditorGUILayout.HorizontalScope("box"))
            {
                GUILayout.Label("StackColor");
                StackColor.colorValue = EditorGUILayout.ColorField(StackColor.colorValue);
                GUILayout.Space(64.0f);
            }
        }

        // ================================================================================================
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        // ================================================================================================

        GUILayout.Label("Texture", EditorStyles.boldLabel);
        using (new EditorGUILayout.VerticalScope("box"))
        {
            GUILayout.Label("Layer0");
            using (new EditorGUILayout.VerticalScope("box"))
            {
                GUILayout.Space(2.0f);
                using (new EditorGUILayout.HorizontalScope())
                {
                    var options = new []{GUILayout.Width (64), GUILayout.Height (64)};
                    using (new EditorGUILayout.VerticalScope())
                    {
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            GUILayout.Label("Layer0 MainTexture");
                        }
                        
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            GUILayout.Label("Height");
                            Layer0Height.floatValue = EditorGUILayout.Slider(Layer0Height.floatValue, Layer0Height.rangeLimits.x, Layer0Height.rangeLimits.y);
                        }
                        
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            GUILayout.Label("Color");
                            Layer0StackColor.colorValue = EditorGUILayout.ColorField(Layer0StackColor.colorValue);
                        }
                    }
                    Layer0.textureValue = EditorGUILayout.ObjectField(Layer0.textureValue, typeof(Texture), false, options) as Texture;
                }
            }
        }

        LayerTextureLayout(materialEditor, UseLayer1, Layer1, Layer1Height, Layer1StackColor, "Layer1");

        LayerTextureLayout(materialEditor, UseLayer2, Layer2, Layer2Height, Layer2StackColor, "Layer2");

        LayerTextureLayout(materialEditor, UseLayer3, Layer3, Layer3Height, Layer3StackColor, "Layer3");

        LayerTextureLayout(materialEditor, UseLayer4, Layer4, Layer4Height, Layer4StackColor, "Layer4");

        using (new EditorGUILayout.VerticalScope("box"))
        {
            GUILayout.Label("BackTexture");
            using (new EditorGUILayout.VerticalScope("box"))
            {
                GUILayout.Space(2.0f);
                using (new EditorGUILayout.HorizontalScope())
                {
                    var options = new []{GUILayout.Width (64), GUILayout.Height (64)};
                    using (new EditorGUILayout.VerticalScope())
                    {
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            GUILayout.Label("BackTexture");
                        }
                        
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            GUILayout.Label("Color");
                            BackTexStackColor.colorValue = EditorGUILayout.ColorField(BackTexStackColor.colorValue);
                        }
                    }
                    BackTex.textureValue = EditorGUILayout.ObjectField(BackTex.textureValue, typeof(Texture), false, options) as Texture;
                }
            }
        }

        // ================================================================================================
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        // ================================================================================================

        GUILayout.Label("Dissolve", EditorStyles.boldLabel);
        using (new EditorGUILayout.VerticalScope("box"))
        {
            using (new EditorGUILayout.VerticalScope("box"))
            {
                GUILayout.Space(2.0f);
                using (new EditorGUILayout.HorizontalScope())
                {
                    var options = new []{GUILayout.Width (64), GUILayout.Height (64)};
                    using (new EditorGUILayout.VerticalScope())
                    {
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            GUILayout.Label("DissolveTexture");
                        }

                        using (new EditorGUILayout.HorizontalScope())
                        {
                            GUILayout.Label("DissolveAmount");
                            DissolveAmount.floatValue = EditorGUILayout.Slider(DissolveAmount.floatValue, DissolveAmount.rangeLimits.x, DissolveAmount.rangeLimits.y);
                        }
                    }
                    DissolveTex.textureValue = EditorGUILayout.ObjectField(DissolveTex.textureValue, typeof(Texture), false, options) as Texture;
                }
            }
        }

        // ================================================================================================
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        // ================================================================================================

        GUILayout.Label("Edge", EditorStyles.boldLabel);
        using (new EditorGUILayout.VerticalScope("box"))
        {
            materialEditor.ShaderProperty(UseEdge, new GUIContent("UseEdge"));
            if(UseEdge.floatValue > 0.0f)
            {
                using (new EditorGUILayout.VerticalScope("box"))
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        GUILayout.Label("EdgeColor");
                        EdgeColor.colorValue = EditorGUILayout.ColorField(EdgeColor.colorValue);
                        GUILayout.Space(64.0f);
                    }
                    
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        GUILayout.Label("EdgeRange");
                        EdgeRange.floatValue = EditorGUILayout.Slider(EdgeRange.floatValue, EdgeRange.rangeLimits.x, EdgeRange.rangeLimits.y);
                        GUILayout.Space(64.0f);
                    }
                
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        GUILayout.Label("EdgeBlur");
                        EdgeBlur.floatValue = EditorGUILayout.Slider(EdgeBlur.floatValue, EdgeBlur.rangeLimits.x, EdgeBlur.rangeLimits.y);
                        GUILayout.Space(64.0f);
                    }
                }
            }
        }
    }

    void LayerTextureLayout(MaterialEditor ed,            MaterialProperty UseLayerToggle, MaterialProperty LayerTex, 
                            MaterialProperty LayerHeight, MaterialProperty LayerCol,       string LayerName)
    {
        using (new EditorGUILayout.VerticalScope("box"))
        {
            ed.ShaderProperty(UseLayerToggle, new GUIContent("Use" + LayerName));
            if(UseLayerToggle.floatValue > 0.0f)
            {
                using (new EditorGUILayout.VerticalScope("box"))
                {
                    GUILayout.Space(2.0f);
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        var options = new []{GUILayout.Width (64), GUILayout.Height (64)};
                        using (new EditorGUILayout.VerticalScope())
                        {
                            GUILayout.Label(LayerName + " MainTexture");
                            using (new EditorGUILayout.HorizontalScope())
                            {
                                GUILayout.Label("Height");
                                LayerHeight.floatValue = EditorGUILayout.Slider(LayerHeight.floatValue, LayerHeight.rangeLimits.x, LayerHeight.rangeLimits.y);
                            }
                            
                            using (new EditorGUILayout.HorizontalScope())
                            {
                                GUILayout.Label("Color");
                                LayerCol.colorValue = EditorGUILayout.ColorField(LayerCol.colorValue);
                            }
                        }
                        LayerTex.textureValue = EditorGUILayout.ObjectField(LayerTex.textureValue, typeof(Texture), false, options) as Texture;
                    }
                }
            }
        }
    }
}


