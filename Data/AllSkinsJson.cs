using CUE4Parse.UE4.Assets.Exports;

namespace ProSwapperLobby.Data
{
    public class AllSkinsJson
    {
        public class Root
        {
            public List<Datum> data { get; set; }
        }

        public class Datum
        {
            public string Key { get; set; }
            public Value[] Value { get; set; }
        }

        public class Value
        {
            public string Type { get; set; }
            public string Name { get; set; }
            public Properties Properties { get; set; }
            public string Outer { get; set; }
        }

        public class Properties
        {
            public Basecharacterpart[] BaseCharacterParts { get; set; }
            public Herodefinition HeroDefinition { get; set; }
            public string Rarity { get; set; }
            public Displayname DisplayName { get; set; }
            public Shortdescription ShortDescription { get; set; }
            public Description Description { get; set; }
            public string[] GameplayTags { get; set; }
            public Itemvariant[] ItemVariants { get; set; }
            public Itemvariantpreview[] ItemVariantPreviews { get; set; }
            public Displayassetpath DisplayAssetPath { get; set; }
            public Partoption[] PartOptions { get; set; }
            public Variantchanneltag VariantChannelTag { get; set; }
            public Variantchannelname VariantChannelName { get; set; }
            public Materialoption[] MaterialOptions { get; set; }
            public Particleoption[] ParticleOptions { get; set; }
            public Itemsearchtag[] ItemSearchTags { get; set; }
            public string[] MetaTags { get; set; }
            public Smallpreviewimage SmallPreviewImage { get; set; }
            public Largepreviewimage LargePreviewImage { get; set; }
            public string[] ObservedPlayerStats { get; set; }
            public string ZerosDigitParamName { get; set; }
            public string TensDigitParamName { get; set; }
            public Materialstoalter2[] MaterialsToAlter { get; set; }
            public string Gender { get; set; }
            public Series Series { get; set; }
            public Builtinemote[] BuiltInEmotes { get; set; }
            public Previewpawnrotationoffset PreviewPawnRotationOffset { get; set; }
            public string VariantUnlockType { get; set; }
            public Inlinevariant InlineVariant { get; set; }
            public float PreviewPawnScale { get; set; }
            public Requiredcosmeticitem[] RequiredCosmeticItems { get; set; }
            public Foleylibrary[] FoleyLibraries { get; set; }
            public Requesteddatastores RequestedDataStores { get; set; }
            public float DefaultStartingValue { get; set; }
            public string MaterialParamName { get; set; }
            public Variantchannelstoneversendtomcp[] VariantChannelsToNeverSendToMCP { get; set; }
            public Itemtexturevar ItemTextureVar { get; set; }
            public int AntiConflictChannel { get; set; }
            public Variantchanneltouseforthumbnails VariantChannelToUseForThumbnails { get; set; }
            public bool bOverrideDefaultVariantPreviewTime { get; set; }
            public float DefaultVariantPreviewOverrideTime { get; set; }
            public Exclusivedesciption ExclusiveDesciption { get; set; }
            public Exclusiveicon ExclusiveIcon { get; set; }
            public Reactivepreviewdrivers ReactivePreviewDrivers { get; set; }
            public Defaultbackpack DefaultBackpack { get; set; }
            public Itempreviewactorclass ItemPreviewActorClass { get; set; }
            public Unlockrequirements1 UnlockRequirements { get; set; }
            public float MaxParamValue { get; set; }
            public Unlockchallengebundle UnlockChallengeBundle { get; set; }
            public string ParticleParamName { get; set; }
            public Particlesystemstoalter[] ParticleSystemsToAlter { get; set; }
            public Itemvariantpreviewgenerator ItemVariantPreviewGenerator { get; set; }
            public Wrapvariant WrapVariant { get; set; }
            public Wrapvariantchanneltag WrapVariantChannelTag { get; set; }
            public Basevariantpreview BaseVariantPreview { get; set; }
            public int NumWrapPreviews { get; set; }
            public Previewwrapdefinition[] PreviewWrapDefinitions { get; set; }
            public string DefaultCustomData { get; set; }
            public Custompreviewtilematerial CustomPreviewTileMaterial { get; set; }
            public float MinimumContrast { get; set; }
            public float MinimumHueShift { get; set; }
            public float MinimumSaturationShift { get; set; }
            public string AntiConflictRules { get; set; }
            public Directaquisitionstyledisclaimeroverride DirectAquisitionStyleDisclaimerOverride { get; set; }
            public bool bNeverPersisted { get; set; }
            public bool bIsShuffleTile { get; set; }
            public Frontendpreviewpivotoffset FrontendPreviewPivotOffset { get; set; }
            public Frontendpreviewinitialrotation FrontendPreviewInitialRotation { get; set; }
            public Frontendpreviewmeshoverride FrontendPreviewMeshOverride { get; set; }
        }

        public class Herodefinition
        {
            public string ObjectName { get; set; }
            public string ObjectPath { get; set; }
        }

        public class Displayname
        {
            public string Namespace { get; set; }
            public string Key { get; set; }
            public string SourceString { get; set; }
        }

        public class Shortdescription
        {
            public string Namespace { get; set; }
            public string Key { get; set; }
            public string SourceString { get; set; }
        }

        public class Description
        {
            public string Namespace { get; set; }
            public string Key { get; set; }
            public string SourceString { get; set; }
        }

        public class Displayassetpath
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Variantchanneltag
        {
            public string TagName { get; set; }
        }

        public class Variantchannelname
        {
            public string Namespace { get; set; }
            public string Key { get; set; }
            public string SourceString { get; set; }
        }

        public class Smallpreviewimage
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Largepreviewimage
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Series
        {
            public string ObjectName { get; set; }
            public string ObjectPath { get; set; }
        }

        public class Previewpawnrotationoffset
        {
            public float Pitch { get; set; }
            public float Yaw { get; set; }
            public float Roll { get; set; }
        }

        public class Inlinevariant
        {
            public Richcolorvar RichColorVar { get; set; }
            public bool bStartUnlocked { get; set; }
            public Customizationvarianttag CustomizationVariantTag { get; set; }
            public bool bIsDefault { get; set; }
            public Variantname VariantName { get; set; }
            public Unlockrequirements UnlockRequirements { get; set; }
        }

        public class Richcolorvar
        {
            public Defaultstartingcolor DefaultStartingColor { get; set; }
            public Materialstoalter[] MaterialsToAlter { get; set; }
            public string ColorParamName { get; set; }
            public Colorswatchforchoices ColorSwatchForChoices { get; set; }
            public Particlestoalter[] ParticlesToAlter { get; set; }
        }

        public class Defaultstartingcolor
        {
            public float R { get; set; }
            public float G { get; set; }
            public float B { get; set; }
            public float A { get; set; }
            public string Hex { get; set; }
        }

        public class Colorswatchforchoices
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Materialstoalter
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Particlestoalter
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Customizationvarianttag
        {
            public string TagName { get; set; }
        }

        public class Variantname
        {
            public string Namespace { get; set; }
            public string Key { get; set; }
            public string SourceString { get; set; }
        }

        public class Unlockrequirements
        {
            public string Namespace { get; set; }
            public string Key { get; set; }
            public string SourceString { get; set; }
        }

        public class Requesteddatastores
        {
            public Mangodatastore MangoDataStore { get; set; }
        }

        public class Mangodatastore
        {
            public string ObjectName { get; set; }
            public string ObjectPath { get; set; }
        }

        public class Itemtexturevar
        {
            public Innerdef InnerDef { get; set; }
            public string[] FilterOutItemsWithTags { get; set; }
            public bool bIsDefault { get; set; }
            public Customizationvarianttag1 CustomizationVariantTag { get; set; }
            public Variantname1 VariantName { get; set; }
        }

        public class Innerdef
        {
            public Materialstoalter1[] MaterialsToAlter { get; set; }
            public string ParamName { get; set; }
            public string DefaultSelectedItem { get; set; }
        }

        public class Materialstoalter1
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Customizationvarianttag1
        {
            public string TagName { get; set; }
        }

        public class Variantname1
        {
            public string CultureInvariantString { get; set; }
        }

        public class Variantchanneltouseforthumbnails
        {
            public string TagName { get; set; }
        }

        public class Exclusivedesciption
        {
            public string Namespace { get; set; }
            public string Key { get; set; }
            public string SourceString { get; set; }
        }

        public class Exclusiveicon
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Reactivepreviewdrivers
        {
        }

        public class Defaultbackpack
        {
            public string ObjectName { get; set; }
            public string ObjectPath { get; set; }
        }

        public class Itempreviewactorclass
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Unlockrequirements1
        {
            public string Namespace { get; set; }
            public string Key { get; set; }
            public string SourceString { get; set; }
        }

        public class Unlockchallengebundle
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Itemvariantpreviewgenerator
        {
            public string ObjectName { get; set; }
            public string ObjectPath { get; set; }
        }

        public class Wrapvariant
        {
            public Innerdef1 InnerDef { get; set; }
            public Customizationvarianttag2 CustomizationVariantTag { get; set; }
        }

        public class Innerdef1
        {
            public string[] ComponentNameAllowList { get; set; }
            public int WrapSectionMask { get; set; }
            public Allowedmesh[] AllowedMeshes { get; set; }
        }

        public class Allowedmesh
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Customizationvarianttag2
        {
            public string TagName { get; set; }
        }

        public class Wrapvariantchanneltag
        {
            public string TagName { get; set; }
        }

        public class Basevariantpreview
        {
            public Variantoption[] VariantOptions { get; set; }
        }

        public class Variantoption
        {
            public object[] OwnedVariantTags { get; set; }
            public Itemvariantisusedfor ItemVariantIsUsedFor { get; set; }
            public string CustomData { get; set; }
            public Variantchanneltag1 VariantChannelTag { get; set; }
            public Activevarianttag ActiveVariantTag { get; set; }
        }

        public class Itemvariantisusedfor
        {
            public string ObjectName { get; set; }
            public string ObjectPath { get; set; }
        }

        public class Variantchanneltag1
        {
            public string TagName { get; set; }
        }

        public class Activevarianttag
        {
            public string TagName { get; set; }
        }

        public class Custompreviewtilematerial
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Directaquisitionstyledisclaimeroverride
        {
            public string Namespace { get; set; }
            public string Key { get; set; }
            public string SourceString { get; set; }
        }

        public class Frontendpreviewpivotoffset
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }
        }

        public class Frontendpreviewinitialrotation
        {
            public float Pitch { get; set; }
            public float Yaw { get; set; }
            public float Roll { get; set; }
        }

        public class Frontendpreviewmeshoverride
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Basecharacterpart
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Itemvariant
        {
            public string ObjectName { get; set; }
            public string ObjectPath { get; set; }
        }

        public class Itemvariantpreview
        {
            public Unlockcondition UnlockCondition { get; set; }
            public float PreviewTime { get; set; }
            public Variantoption1[] VariantOptions { get; set; }
            public Additionalitem[] AdditionalItems { get; set; }
        }

        public class Unlockcondition
        {
            public object CultureInvariantString { get; set; }
            public string Namespace { get; set; }
            public string Key { get; set; }
            public string SourceString { get; set; }
        }

        public class Variantoption1
        {
            public string[] OwnedVariantTags { get; set; }
            public Itemvariantisusedfor1 ItemVariantIsUsedFor { get; set; }
            public string CustomData { get; set; }
            public Variantchanneltag2 VariantChannelTag { get; set; }
            public Activevarianttag1 ActiveVariantTag { get; set; }
        }

        public class Itemvariantisusedfor1
        {
            public string ObjectName { get; set; }
            public string ObjectPath { get; set; }
        }

        public class Variantchanneltag2
        {
            public string TagName { get; set; }
        }

        public class Activevarianttag1
        {
            public string TagName { get; set; }
        }

        public class Additionalitem
        {
            public object[] VariantOptions { get; set; }
            public Item Item { get; set; }
        }

        public class Item
        {
            public string ObjectName { get; set; }
            public string ObjectPath { get; set; }
        }

        public class Partoption
        {
            public Variantpart[] VariantParts { get; set; }
            public Variantmaterial[] VariantMaterials { get; set; }
            public Variantmaterialparam[] VariantMaterialParams { get; set; }
            public object[] InitalParticleSystemData { get; set; }
            public Variantparticle[] VariantParticles { get; set; }
            public Variantparticleparam[] VariantParticleParams { get; set; }
            public object[] VariantSounds { get; set; }
            public Variantfoley[] VariantFoley { get; set; }
            public Sockettransform[] SocketTransforms { get; set; }
            public object[] VariantActions { get; set; }
            public object[] VariantMeshes { get; set; }
            public Metatags MetaTags { get; set; }
            public bool bStartUnlocked { get; set; }
            public bool bIsDefault { get; set; }
            public bool bHideIfNotOwned { get; set; }
            public Customizationvarianttag3 CustomizationVariantTag { get; set; }
            public Variantname2 VariantName { get; set; }
            public Previewimage PreviewImage { get; set; }
            public Unlockrequirements2 UnlockRequirements { get; set; }
            public Unlockingitemdef UnlockingItemDef { get; set; }
        }

        public class Metatags
        {
            public string[] MetaTagsToApply { get; set; }
            public string[] MetaTagsToRemove { get; set; }
        }

        public class Customizationvarianttag3
        {
            public string TagName { get; set; }
        }

        public class Variantname2
        {
            public string Namespace { get; set; }
            public string Key { get; set; }
            public string SourceString { get; set; }
            public object CultureInvariantString { get; set; }
        }

        public class Previewimage
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Unlockrequirements2
        {
            public object CultureInvariantString { get; set; }
            public string Namespace { get; set; }
            public string Key { get; set; }
            public string SourceString { get; set; }
        }

        public class Unlockingitemdef
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Variantpart
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Variantmaterial
        {
            public Materialtoswap MaterialToSwap { get; set; }
            public string ComponentToOverride { get; set; }
            public string CascadeMaterialName { get; set; }
            public int MaterialOverrideIndex { get; set; }
            public Overridematerial OverrideMaterial { get; set; }
        }

        public class Materialtoswap
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Overridematerial
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Variantmaterialparam
        {
            public Materialtoalter MaterialToAlter { get; set; }
            public string CascadeMaterialName { get; set; }
            public Colorparam[] ColorParams { get; set; }
            public Textureparam[] TextureParams { get; set; }
            public Floatparam[] FloatParams { get; set; }
        }

        public class Materialtoalter
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Colorparam
        {
            public Value1 Value { get; set; }
            public string ParamName { get; set; }
        }

        public class Value1
        {
            public float R { get; set; }
            public float G { get; set; }
            public float B { get; set; }
            public float A { get; set; }
            public string Hex { get; set; }
        }

        public class Textureparam
        {
            public Value2 Value { get; set; }
            public string ParamName { get; set; }
        }

        public class Value2
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Floatparam
        {
            public float Value { get; set; }
            public string ParamName { get; set; }
        }

        public class Variantparticle
        {
            public Particlesystemtoalter ParticleSystemToAlter { get; set; }
            public string ComponentToOverride { get; set; }
            public Overrideparticlesystem OverrideParticleSystem { get; set; }
            public bool bResetParameterOverridesOnSwap { get; set; }
        }

        public class Particlesystemtoalter
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Overrideparticlesystem
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Variantparticleparam
        {
            public Particlesystemtoalter1 ParticleSystemToAlter { get; set; }
            public Colorparam1[] ColorParams { get; set; }
            public object[] VectorParams { get; set; }
            public Floatparam1[] FloatParams { get; set; }
        }

        public class Particlesystemtoalter1
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Colorparam1
        {
            public Value3 Value { get; set; }
            public string ParamName { get; set; }
        }

        public class Value3
        {
            public float R { get; set; }
            public float G { get; set; }
            public float B { get; set; }
            public float A { get; set; }
            public string Hex { get; set; }
        }

        public class Floatparam1
        {
            public float Value { get; set; }
            public string ParamName { get; set; }
        }

        public class Variantfoley
        {
            public Librariestoadd[] LibrariesToAdd { get; set; }
            public Librariestoremove[] LibrariesToRemove { get; set; }
        }

        public class Librariestoadd
        {
            public string ObjectName { get; set; }
            public string ObjectPath { get; set; }
        }

        public class Librariestoremove
        {
            public string ObjectName { get; set; }
            public string ObjectPath { get; set; }
        }

        public class Sockettransform
        {
            public string SourceSocketName { get; set; }
            public string OverridSocketName { get; set; }
            public Sourceobjecttomodify SourceObjectToModify { get; set; }
        }

        public class Sourceobjecttomodify
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Materialoption
        {
            public Variantmaterial1[] VariantMaterials { get; set; }
            public Variantmaterialparam1[] VariantMaterialParams { get; set; }
            public object[] VariantSounds { get; set; }
            public object[] VariantFoley { get; set; }
            public Metatags1 MetaTags { get; set; }
            public bool bStartUnlocked { get; set; }
            public bool bIsDefault { get; set; }
            public bool bHideIfNotOwned { get; set; }
            public Customizationvarianttag4 CustomizationVariantTag { get; set; }
            public Variantname3 VariantName { get; set; }
            public Previewimage1 PreviewImage { get; set; }
            public Unlockrequirements3 UnlockRequirements { get; set; }
            public Unlockingitemdef1 UnlockingItemDef { get; set; }
        }

        public class Metatags1
        {
            public string[] MetaTagsToApply { get; set; }
            public object[] MetaTagsToRemove { get; set; }
        }

        public class Customizationvarianttag4
        {
            public string TagName { get; set; }
        }

        public class Variantname3
        {
            public string Namespace { get; set; }
            public string Key { get; set; }
            public string SourceString { get; set; }
            public object CultureInvariantString { get; set; }
        }

        public class Previewimage1
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Unlockrequirements3
        {
            public object CultureInvariantString { get; set; }
            public string Namespace { get; set; }
            public string Key { get; set; }
            public string SourceString { get; set; }
        }

        public class Unlockingitemdef1
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Variantmaterial1
        {
            public Materialtoswap1 MaterialToSwap { get; set; }
            public string ComponentToOverride { get; set; }
            public string CascadeMaterialName { get; set; }
            public int MaterialOverrideIndex { get; set; }
            public Overridematerial1 OverrideMaterial { get; set; }
        }

        public class Materialtoswap1
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Overridematerial1
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Variantmaterialparam1
        {
            public Materialtoalter1 MaterialToAlter { get; set; }
            public string CascadeMaterialName { get; set; }
            public Colorparam2[] ColorParams { get; set; }
            public Textureparam1[] TextureParams { get; set; }
            public Floatparam2[] FloatParams { get; set; }
        }

        public class Materialtoalter1
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Colorparam2
        {
            public Value4 Value { get; set; }
            public string ParamName { get; set; }
        }

        public class Value4
        {
            public float R { get; set; }
            public float G { get; set; }
            public float B { get; set; }
            public float A { get; set; }
            public string Hex { get; set; }
        }

        public class Textureparam1
        {
            public Value5 Value { get; set; }
            public string ParamName { get; set; }
        }

        public class Value5
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Floatparam2
        {
            public float Value { get; set; }
            public string ParamName { get; set; }
        }

        public class Particleoption
        {
            public Variantmaterial2[] VariantMaterials { get; set; }
            public Variantmaterialparam2[] VariantMaterialParams { get; set; }
            public object[] InitialParticleSystemData { get; set; }
            public Variantparticle1[] VariantParticles { get; set; }
            public Variantparticleparam1[] VariantParticleParams { get; set; }
            public object[] VariantSounds { get; set; }
            public Metatags2 MetaTags { get; set; }
            public bool bStartUnlocked { get; set; }
            public bool bIsDefault { get; set; }
            public bool bHideIfNotOwned { get; set; }
            public Customizationvarianttag5 CustomizationVariantTag { get; set; }
            public Variantname4 VariantName { get; set; }
            public Previewimage2 PreviewImage { get; set; }
            public Unlockrequirements4 UnlockRequirements { get; set; }
            public Unlockingitemdef2 UnlockingItemDef { get; set; }
        }

        public class Metatags2
        {
            public object[] MetaTagsToApply { get; set; }
            public object[] MetaTagsToRemove { get; set; }
        }

        public class Customizationvarianttag5
        {
            public string TagName { get; set; }
        }

        public class Variantname4
        {
            public string Namespace { get; set; }
            public string Key { get; set; }
            public string SourceString { get; set; }
        }

        public class Previewimage2
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Unlockrequirements4
        {
            public object CultureInvariantString { get; set; }
            public string Namespace { get; set; }
            public string Key { get; set; }
            public string SourceString { get; set; }
        }

        public class Unlockingitemdef2
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Variantmaterial2
        {
            public Materialtoswap2 MaterialToSwap { get; set; }
            public string ComponentToOverride { get; set; }
            public string CascadeMaterialName { get; set; }
            public int MaterialOverrideIndex { get; set; }
            public Overridematerial2 OverrideMaterial { get; set; }
        }

        public class Materialtoswap2
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Overridematerial2
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Variantmaterialparam2
        {
            public Materialtoalter2 MaterialToAlter { get; set; }
            public string CascadeMaterialName { get; set; }
            public Colorparam3[] ColorParams { get; set; }
            public Textureparam2[] TextureParams { get; set; }
            public Floatparam3[] FloatParams { get; set; }
        }

        public class Materialtoalter2
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Colorparam3
        {
            public Value6 Value { get; set; }
            public string ParamName { get; set; }
        }

        public class Value6
        {
            public float R { get; set; }
            public float G { get; set; }
            public float B { get; set; }
            public float A { get; set; }
            public string Hex { get; set; }
        }

        public class Textureparam2
        {
            public Value7 Value { get; set; }
            public string ParamName { get; set; }
        }

        public class Value7
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Floatparam3
        {
            public float Value { get; set; }
            public string ParamName { get; set; }
        }

        public class Variantparticle1
        {
            public Particlesystemtoalter2 ParticleSystemToAlter { get; set; }
            public string ComponentToOverride { get; set; }
            public Overrideparticlesystem1 OverrideParticleSystem { get; set; }
            public bool bResetParameterOverridesOnSwap { get; set; }
        }

        public class Particlesystemtoalter2
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Overrideparticlesystem1
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Variantparticleparam1
        {
            public Particlesystemtoalter3 ParticleSystemToAlter { get; set; }
            public Colorparam4[] ColorParams { get; set; }
            public Vectorparam[] VectorParams { get; set; }
            public Floatparam4[] FloatParams { get; set; }
        }

        public class Particlesystemtoalter3
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Colorparam4
        {
            public Value8 Value { get; set; }
            public string ParamName { get; set; }
        }

        public class Value8
        {
            public float R { get; set; }
            public float G { get; set; }
            public float B { get; set; }
            public float A { get; set; }
            public string Hex { get; set; }
        }

        public class Vectorparam
        {
            public Value9 Value { get; set; }
            public string ParamName { get; set; }
        }

        public class Value9
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }
        }

        public class Floatparam4
        {
            public float Value { get; set; }
            public string ParamName { get; set; }
        }

        public class Itemsearchtag
        {
            public string Namespace { get; set; }
            public string Key { get; set; }
            public string SourceString { get; set; }
        }

        public class Materialstoalter2
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Builtinemote
        {
            public string ObjectName { get; set; }
            public string ObjectPath { get; set; }
        }

        public class Requiredcosmeticitem
        {
            public string ObjectName { get; set; }
            public string ObjectPath { get; set; }
        }

        public class Foleylibrary
        {
            public string ObjectName { get; set; }
            public string ObjectPath { get; set; }
        }

        public class Variantchannelstoneversendtomcp
        {
            public string TagName { get; set; }
        }

        public class Particlesystemstoalter
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

        public class Previewwrapdefinition
        {
            public string AssetPathName { get; set; }
            public string SubPathString { get; set; }
        }

    }
}
