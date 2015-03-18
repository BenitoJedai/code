using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms
{
	public static class References
	{
		// https://github.com/mrdoob/three.js/wiki/How-to-use-OpenGL-or-ANGLE-rendering-on-Windows
		// chrome.exe --use-gl=desktop
		//		GL_RENDERER ANGLE(Intel(R) HD Graphics Family Direct3D11 vs_5_0 ps_5_0)
		//GL_VERSION OpenGL ES 2.0 (ANGLE 2.1.524e3bde19d0)


		// http://on-demand.gputechconf.com/gtc/2014/video/S4550-shadertoy-fragment-shader.mp4

		// http://stackoverflow.com/questions/17047602/proper-way-to-initialize-a-c-sharp-dictionary-with-values-already-in-it
		// on nexus 9 we should lear to react to touch move
		// can we get a continious feedback loop going where 
		// we could add content from the live version of the debugged app?
		// like describe what we are seeing and have it stored as source code for the next iteration
		public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
		{
			// should we want to generate it?

			// group by runs on all devices, fps?
			// tags?

			//  /FilterTo:$(SolutionDir)
			// how will those shaders look like on VR?

			// this is special
			["ChromeShaderToySimpleLoadingScreenByNdel"] = () => new ChromeShaderToySimpleLoadingScreenByNdel.Shaders.ProgramFragmentShader(),

			["ChromeShaderToyColumns"] = () => new ChromeShaderToyColumns.Shaders.ProgramFragmentShader(),

			// can we have alt-tab pip ?

			["AngelsByIq"] = () => new AngelsByIq.Shaders.ProgramFragmentShader(),
			["ApollonianByIq"] = () => new ApollonianByIq.Shaders.ProgramFragmentShader(),
			["AttackOfTheFuzziesByEiffie"] = () => new AttackOfTheFuzziesByEiffie.Shaders.ProgramFragmentShader(),
			["AtticByRobert"] = () => new AtticByRobert.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyAnalyticalMotionblurByIq"] = () => new ChromeShaderToyAnalyticalMotionblurByIq.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyAlbertArchesByDr2"] = () => new ChromeShaderToyAlbertArchesByDr2.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyAlpsByHoskins"] = () => new ChromeShaderToyAlpsByHoskins.Shaders.ProgramFragmentShader(),
			// crashes nexus?
			//["ChromeShaderToyAndroidsByGreen"] = () => new ChromeShaderToyAndroidsByGreen.Shaders.ProgramFragmentShader(),
			//["ChromeShaderToyAnimationByFlyguy"] = () => new ChromeShaderToyAnimationByFlyguy.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyAssBlockByDila"] = () => new ChromeShaderToyAssBlockByDila.Shaders.ProgramFragmentShader(),


			["BatsByGaz"] = () => new BatsByGaz.Shaders.ProgramFragmentShader(),
			["BlueWallClockByC3d"] = () => new BlueWallClockByC3d.Shaders.ProgramFragmentShader(),
			["BoingBallByUnitZeroOne"] = () => new BoingBallByUnitZeroOne.Shaders.ProgramFragmentShader(),
			["BokehBlurByKabuto"] = () => new ChromeShaderToyBokehBlurByKabuto.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyBenderByIq"] = () => new ChromeShaderToyBenderByIq.Shaders.ProgramFragmentShader(),

			["ChainReactionByEiffie"] = () => new ChainReactionByEiffie.Shaders.ProgramFragmentShader(),
			["CheesyByPMalin"] = () => new CheesyByPMalin.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyCarcarspacecarByEiffie"] = () => new ChromeShaderToyCarcarspacecarByEiffie.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyCatchByAhihi"] = () => new ChromeShaderToyCatchByAhihi.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyCCLatticesByPaniq"] = () => new ChromeShaderToyCCLatticesByPaniq.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyCentaursByErucipe"] = () => new ChromeShaderToyCentaursByErucipe.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyChainsGearsByPMalin"] = () => new ChromeShaderToyChainsGearsByPMalin.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyCookTorranceByXbe"] = () => new ChromeShaderToyCookTorranceByXbe.Shaders.ProgramFragmentShader(),
			// crashes nexus?
			["ChromeShaderToyCubeOfCubesByFlyguy"] = () => new ChromeShaderToyCubeOfCubesByFlyguy.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyCubicEntanglementByEiffie"] = () => new ChromeShaderToyCubicEntanglementByEiffie.Shaders.ProgramFragmentShader(),

			// crashes nexus?
			["ChromeShaderToyDancingViriiByEntropyNine"] = () => new ChromeShaderToyDancingViriiByEntropyNine.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyDesertMorningByEPitz"] = () => new ChromeShaderToyDesertMorningByEPitz.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyDepthByGreen"] = () => new ChromeShaderToyDepthByGreen.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyDiamondsForeverByNrx"] = () => new ChromeShaderToyDiamondsForeverByNrx.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyDiningRoomBySquid"] = () => new ChromeShaderToyDiningRoomBySquid.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyDigitalHeartByJoshp"] = () => new ChromeShaderToyDigitalHeartByJoshp.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyDigitsByFabrice"] = () => new ChromeShaderToyDigitsByFabrice.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyDistanceFieldBlurByTekF"] = () => new ChromeShaderToyDistanceFieldBlurByTekF.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyDoomE1M1ByPMalin"] = () => new ChromeShaderToyDoomE1M1ByPMalin.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyDustStormByNimitz"] = () => new ChromeShaderToyDustStormByNimitz.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyDustyByHat"] = () => new ChromeShaderToyDustyByHat.Shaders.ProgramFragmentShader(),
			["DataTransferBySrtuss"] = () => new DataTransferBySrtuss.Shaders.ProgramFragmentShader(),
			["DFLightingByTekF"] = () => new DFLightingByTekF.Shaders.ProgramFragmentShader(),
			["DigitalClockByAndre"] = () => new DigitalClockByAndre.Shaders.ProgramFragmentShader(),
			["DirectLightUsingMISByKoiava"] = () => new DirectLightUsingMISByKoiava.Shaders.ProgramFragmentShader(),
			["DNAByOtavio"] = () => new DNAByOtavio.Shaders.ProgramFragmentShader(),
			["Doom2ByReinder"] = () => new Doom2ByReinder.Shaders.ProgramFragmentShader(),
			["DubstepByAssByRez"] = () => new DubstepByAssByRez.Shaders.ProgramFragmentShader(),


			["ChromeShaderToyEdgeAAByTrisomie"] = () => new ChromeShaderToyEdgeAAByTrisomie.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyEiffieBallsByEiffie"] = () => new ChromeShaderToyEiffieBallsByEiffie.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyEiffieBox"] = () => new ChromeShaderToyEiffieBox.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyExplosionByGreen"] = () => new ChromeShaderToyExplosionByGreen.Shaders.ProgramFragmentShader(),
			["ElectroPrimByAlex"] = () => new ElectroPrimByAlex.Shaders.ProgramFragmentShader(),
			["EscherPlanariaByMattz"] = () => new EscherPlanariaByMattz.Shaders.ProgramFragmentShader(),

			["ChromeShaderToyFastBallsByIq"] = () => new ChromeShaderToyFastBallsByIq.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyFastEdgeDetectionByNimitz"] = () => new ChromeShaderToyFastEdgeDetectionByNimitz.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyFireballByGreen"] = () => new ChromeShaderToyFireballByGreen.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyFireCounterByFabrice"] = () => new ChromeShaderToyFireCounterByFabrice.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyFlowingLavaByFizzer"] = () => new ChromeShaderToyFlowingLavaByFizzer.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyFlyingBoatByGaz"] = () => new ChromeShaderToyFlyingBoatByGaz.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyFogMountainByESpitz"] = () => new ChromeShaderToyFogMountainByESpitz.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyFrostedTorusByPwd"] = () => new ChromeShaderToyFrostedTorusByPwd.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyFrozenCrytekLogo"] = () => new ChromeShaderToyFrozenCrytekLogo.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyFrozenWastelandByDave"] = () => new ChromeShaderToyFrozenWastelandByDave.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyFutureBillboardByEiffie"] = () => new ChromeShaderToyFutureBillboardByEiffie.Shaders.ProgramFragmentShader(),
			["FakeVolumeLightByEiffie"] = () => new FakeVolumeLightByEiffie.Shaders.ProgramFragmentShader(),
			["FerrisWheelByGaz"] = () => new FerrisWheelByGaz.Shaders.ProgramFragmentShader(),
			["FlatlandByPMalin"] = () => new FlatlandByPMalin.Shaders.ProgramFragmentShader(),
			["FloatByAlex"] = () => new FloatByAlex.Shaders.ProgramFragmentShader(),
			["FloatPrintByCasty"] = () => new FloatPrintByCasty.Shaders.ProgramFragmentShader(),
			["FractalCondosByEiffie"] = () => new FractalCondosByEiffie.Shaders.ProgramFragmentShader(),
			["FractalSphereByGuil"] = () => new FractalSphereByGuil.Shaders.ProgramFragmentShader(),

			["ChromeShaderToyGmetaballsByGermangb"] = () => new ChromeShaderToyGmetaballsByGermangb.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyGlassPolyhedronByNrx"] = () => new ChromeShaderToyGlassPolyhedronByNrx.Shaders.ProgramFragmentShader(),
			["GeneratorsByKali"] = () => new GeneratorsByKali.Shaders.ProgramFragmentShader(),
			["GlxgearsByBear"] = () => new GlxgearsByBear.Shaders.ProgramFragmentShader(),
			["GraphingByNimitz"] = () => new GraphingByNimitz.Shaders.ProgramFragmentShader(),

			// alpha! via discard
			["ChromeShaderToyHardEdgeShadowByGltracy"] = () => new ChromeShaderToyHardEdgeShadowByGltracy.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyHetchyScketchyByXbe"] = () => new ChromeShaderToyHetchyScketchyByXbe.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyHolographicByTdm"] = () => new ChromeShaderToyHolographicByTdm.Shaders.ProgramFragmentShader(),

			["ChromeShaderToyInfiniteRepetitionBySsdsa"] = () => new ChromeShaderToyInfiniteRepetitionBySsdsa.Shaders.ProgramFragmentShader(),
			["IKSolverByIq"] = () => new IKSolverByIq.Shaders.ProgramFragmentShader(),
			["InputTimeByIq"] = () => new InputTimeByIq.Shaders.ProgramFragmentShader(),
			["InversionMachineByKali"] = () => new InversionMachineByKali.Shaders.ProgramFragmentShader(),
			["IslandByVanburgler"] = () => new IslandByVanburgler.Shaders.ProgramFragmentShader(),

			["ChromeShaderToyKajastusByMarken"] = () => new ChromeShaderToyKajastusByMarken.Shaders.ProgramFragmentShader(),

			["ChromeShaderToyLavaDripByFabrice"] = () => new ChromeShaderToyLavaDripByFabrice.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyLightThornByVlad"] = () => new ChromeShaderToyLightThornByVlad.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyLightCycleByGreen"] = () => new ChromeShaderToyLightCycleByGreen.Shaders.ProgramFragmentShader(),

			// 32 crash xt7
			["ChromeShaderToyLimboByDaeken"] = () => new ChromeShaderToyLimboByDaeken.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyLittleMonsterByHLorenzi"] = () => new ChromeShaderToyLittleMonsterByHLorenzi.Shaders.ProgramFragmentShader(),
			["LineIntersectionByThe23"] = () => new LineIntersectionByThe23.Shaders.ProgramFragmentShader(),
			["LittleFluffyCloudsByGreen"] = () => new LittleFluffyCloudsByGreen.Shaders.ProgramFragmentShader(),

			["ChromeShaderToyllamelsByEiffie"] = () => new ChromeShaderToyllamelsByEiffie.Shaders.ProgramFragmentShader(),

			["ChromeShaderToyMarchingCubesByFizzer"] = () => new ChromeShaderToyMarchingCubesByFizzer.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyMarkersByRougier"] = () => new ChromeShaderToyMarkersByRougier.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyMaterialMenuByTekF"] = () => new ChromeShaderToyMaterialMenuByTekF.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyMinecraftBlocksByReinder"] = () => new ChromeShaderToyMinecraftBlocksByReinder.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyMetatunnelByDuprat"] = () => new ChromeShaderToyMetatunnelByDuprat.Shaders.ProgramFragmentShader(),
			["MarbleSculptureByTekF"] = () => new MarbleSculptureByTekF.Shaders.ProgramFragmentShader(),
			["MikeByIq"] = () => new MikeByIq.Shaders.ProgramFragmentShader(),
			["MissileGameByAsti"] = () => new MissileGameByAsti.Shaders.ProgramFragmentShader(),
			["MirrorRoomByDiLemming"] = () => new MirrorRoomByDiLemming.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyMorningCityByDevin"] = () => new ChromeShaderToyMorningCityByDevin.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyMotionBlurByKig"] = () => new ChromeShaderToyMotionBlurByKig.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyMountainsByHoskins"] = () => new ChromeShaderToyMountainsByHoskins.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyMorphingTeapotByIapafoto"] = () => new ChromeShaderToyMorphingTeapotByIapafoto.Shaders.ProgramFragmentShader(),
			["MonumentValleyByGltracy"] = () => new MonumentValleyByGltracy.Shaders.ProgramFragmentShader(),

			["ChromeShaderToyNeonParallaxByNimitz"] = () => new ChromeShaderToyNeonParallaxByNimitz.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyNoiseDistributionsByHornet"] = () => new ChromeShaderToyNoiseDistributionsByHornet.Shaders.ProgramFragmentShader(),
			["NanoTubesByTrisomie"] = () => new NanoTubesByTrisomie.Shaders.ProgramFragmentShader(),
			["NSAEyeballByEiffie"] = () => new NSAEyeballByEiffie.Shaders.ProgramFragmentShader(),
			["NumbersByPMalin"] = () => new NumbersByPMalin.Shaders.ProgramFragmentShader(),


			["ChromeShaderToyOblivionByNimitz"] = () => new ChromeShaderToyOblivionByNimitz.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyOcclusionClippingByIq"] = () => new ChromeShaderToyOcclusionClippingByIq.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyOculusTestByDaeken"] = () => new ChromeShaderToyOculusTestByDaeken.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyOcuLimboByDaeken"] = () => new ChromeShaderToyOcuLimboByDaeken.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyOnOffSpikesByMovAX13h"] = () => new ChromeShaderToyOnOffSpikesByMovAX13h.Shaders.ProgramFragmentShader(),
			["OrchardNightByEiffie"] = () => new OrchardNightByEiffie.Shaders.ProgramFragmentShader(),

			["ChromeShaderToyPangramByHoskins"] = () => new ChromeShaderToyPangramByHoskins.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyPhysicsNoCollisionsByIq"] = () => new ChromeShaderToyPhysicsNoCollisionsByIq.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyPianoByIq"] = () => new ChromeShaderToyPianoByIq.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyPigsRunningByGaz"] = () => new ChromeShaderToyPigsRunningByGaz.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyPlasmaTriangleByElusivePete"] = () => new ChromeShaderToyPlasmaTriangleByElusivePete.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyPortalByHLorenzi"] = () => new ChromeShaderToyPortalByHLorenzi.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyPortalTurretByMattz"] = () => new ChromeShaderToyPortalTurretByMattz.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyPolygonalTerrainByFizzer"] = () => new ChromeShaderToyPolygonalTerrainByFizzer.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyPrimitivesByQuilez"] = () => new ChromeShaderToyPrimitivesByQuilez.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyPyramidsByAvix"] = () => new ChromeShaderToyPyramidsByAvix.Shaders.ProgramFragmentShader(),
			["PalmettoStalkByEiffie"] = () => new PalmettoStalkByEiffie.Shaders.ProgramFragmentShader(),
			["PlayingWithRefleksByEiffie"] = () => new PlayingWithRefleksByEiffie.Shaders.ProgramFragmentShader(),
			["PopularShaderByFizzer"] = () => new PopularShaderByFizzer.Shaders.ProgramFragmentShader(),
			["PrairieByEiffie"] = () => new PrairieByEiffie.Shaders.ProgramFragmentShader(),
			["PreloaderByMattdesl"] = () => new PreloaderByMattdesl.Shaders.ProgramFragmentShader(),
			["PseudoArmillaryTestByRK"] = () => new PseudoArmillaryTestByRK.Shaders.ProgramFragmentShader(),

			["ChromeShaderToyQuadraticBezierByMattdesl"] = () => new ChromeShaderToyQuadraticBezierByMattdesl.Shaders.ProgramFragmentShader(),
			["QuadraticBezierByIq"] = () => new QuadraticBezierByIq.Shaders.ProgramFragmentShader(),

			["ChromeShaderToyRavingErnieByBero"] = () => new ChromeShaderToyRavingErnieByBero.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRayFogByDemofox"] = () => new ChromeShaderToyRayFogByDemofox.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRacingGameByEiffie"] = () => new ChromeShaderToyRacingGameByEiffie.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRefractiveSpheresByKig"] = () => new ChromeShaderToyRefractiveSpheresByKig.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRedditAlienByGleurop"] = () => new ChromeShaderToyRedditAlienByGleurop.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRaymarchByElias"] = () => new ChromeShaderToyRaymarchByElias.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRaymarchEdgeDetectionByHLorenzi"] = () => new ChromeShaderToyRaymarchEdgeDetectionByHLorenzi.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRaymarchingExampleByJack"] = () => new ChromeShaderToyRaymarchingExampleByJack.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRecogniserByFizzer"] = () => new ChromeShaderToyRecogniserByFizzer.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRefractionByHLorenzi"] = () => new ChromeShaderToyRefractionByHLorenzi.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRelentlessBySrtuss"] = () => new ChromeShaderToyRelentlessBySrtuss.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRockyCoast"] = () => new ChromeShaderToyRockyCoast.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRollingBallByHoskins"] = () => new ChromeShaderToyRollingBallByHoskins.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRotatePyramidByGyabo"] = () => new ChromeShaderToyRotatePyramidByGyabo.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRotatingByGa2arch"] = () => new ChromeShaderToyRotatingByGa2arch.Shaders.ProgramFragmentShader(),
			["RayBertByHoskins"] = () => new RayBertByHoskins.Shaders.ProgramFragmentShader(),
			["RayConeRayFrustumByRobert"] = () => new RayConeRayFrustumByRobert.Shaders.ProgramFragmentShader(),
			["RedCellsByPMalin"] = () => new RedCellsByPMalin.Shaders.ProgramFragmentShader(),
			["RefelectingCubeByTriggerHLM"] = () => new RefelectingCubeByTriggerHLM.Shaders.ProgramFragmentShader(),
			["RoadToHellByRez"] = () => new RoadToHellByRez.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRubikSolverByKali"] = () => new ChromeShaderToyRubikSolverByKali.Shaders.ProgramFragmentShader(),

			["ChromeShaderToySadRobotByGreen"] = () => new ChromeShaderToySadRobotByGreen.Shaders.ProgramFragmentShader(),
			["ChromeShaderToySandDunesByBoinx"] = () => new ChromeShaderToySandDunesByBoinx.Shaders.ProgramFragmentShader(),
			["ChromeShaderToySeascapeByTDM"] = () => new ChromeShaderToySeascapeByTDM.Shaders.ProgramFragmentShader(),
			["ChromeShaderToySeabirdsAtSunsetByDr2"] = () => new ChromeShaderToySeabirdsAtSunsetByDr2.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyShadyBuildingByZtri"] = () => new ChromeShaderToyShadyBuildingByZtri.Shaders.ProgramFragmentShader(),
			["ChromeShaderToySomewhere1993ByNimitz"] = () => new ChromeShaderToySomewhere1993ByNimitz.Shaders.ProgramFragmentShader(),
			["ChromeShaderToySpaceRaceByKali"] = () => new ChromeShaderToySpaceRaceByKali.Shaders.ProgramFragmentShader(),
			["ChromeShaderToySphereAndWalls"] = () => new ChromeShaderToySphereAndWalls.Shaders.ProgramFragmentShader(),
			["ChromeShaderToySphereProjectionByIq"] = () => new ChromeShaderToySphereProjectionByIq.Shaders.ProgramFragmentShader(),
			["ChromeShaderToySpitfirePursuitByDr2"] = () => new ChromeShaderToySpitfirePursuitByDr2.Shaders.ProgramFragmentShader(),
			["ChromeShaderToySymmetricOriginsByGood"] = () => new ChromeShaderToySymmetricOriginsByGood.Shaders.ProgramFragmentShader(),
			["ChromeShaderToySubmarinePillars"] = () => new ChromeShaderToySubmarinePillars.Shaders.ProgramFragmentShader(),
			["ChromeShaderToySuperMarioByHLorenzi"] = () => new ChromeShaderToySuperMarioByHLorenzi.Shaders.ProgramFragmentShader(),
			["ChromeShaderToySnowByUggway"] = () => new ChromeShaderToySnowByUggway.Shaders.ProgramFragmentShader(),
			["SansNormalByEiffie"] = () => new SansNormalByEiffie.Shaders.ProgramFragmentShader(),
			["ShaderingChameleonByIapafoto"] = () => new ShaderingChameleonByIapafoto.Shaders.ProgramFragmentShader(),
			["ShadeyMcShadishByDcoombes"] = () => new ShadeyMcShadishByDcoombes.Shaders.ProgramFragmentShader(),
			["ShakespeareQuestByEiffie"] = () => new ShakespeareQuestByEiffie.Shaders.ProgramFragmentShader(),
			["ShapeshifterByMu6k"] = () => new ShapeshifterByMu6k.Shaders.ProgramFragmentShader(),
			["SHVisualizerByIq"] = () => new SHVisualizerByIq.Shaders.ProgramFragmentShader(),
			["SierpinskiByIq"] = () => new SierpinskiByIq.Shaders.ProgramFragmentShader(),
			["SiggraphLogoByIq"] = () => new SiggraphLogoByIq.Shaders.ProgramFragmentShader(),
			["SimpleDigitalClockByMikeCAT"] = () => new SimpleDigitalClockByMikeCAT.Shaders.ProgramFragmentShader(),
			["SimpleVoxelsByElias"] = () => new SimpleVoxelsByElias.Shaders.ProgramFragmentShader(),
			["SinMountainsByFred"] = () => new SinMountainsByFred.Shaders.ProgramFragmentShader(),
			["SomedayByEiffie"] = () => new SomedayByEiffie.Shaders.ProgramFragmentShader(),
			["SomeSortOfGridByGermangb"] = () => new SomeSortOfGridByGermangb.Shaders.ProgramFragmentShader(),
			["SoundAcidJamBySrtuss"] = () => new SoundAcidJamBySrtuss.Shaders.ProgramFragmentShader(),
			["SparseGridMarchingByNimitz"] = () => new SparseGridMarchingByNimitz.Shaders.ProgramFragmentShader(),
			["SphereMappingsByNimitz"] = () => new SphereMappingsByNimitz.Shaders.ProgramFragmentShader(),
			["StairwayToHeavenByEiffie"] = () => new StairwayToHeavenByEiffie.Shaders.ProgramFragmentShader(),
			["SteamLogoByYakoudbz"] = () => new SteamLogoByYakoudbz.Shaders.ProgramFragmentShader(),

			["ChromeShaderToyTextCandyByCPU"] = () => new ChromeShaderToyTextCandyByCPU.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyTexturedEllipsoidsByFabrice"] = () => new ChromeShaderToyTexturedEllipsoidsByFabrice.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyTokyoByReinder"] = () => new ChromeShaderToyTokyoByReinder.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyTrainByDr2"] = () => new ChromeShaderToyTrainByDr2.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyTrainRideByDr2"] = () => new ChromeShaderToyTrainRideByDr2.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyTriangleDistanceByIq"] = () => new ChromeShaderToyTriangleDistanceByIq.Shaders.ProgramFragmentShader(),
			["TetrahedralInterpolationByPaniq"] = () => new TetrahedralInterpolationByPaniq.Shaders.ProgramFragmentShader(),
			["TetrahedronatorByEiffie"] = () => new TetrahedronatorByEiffie.Shaders.ProgramFragmentShader(),
			["TileableWaterCausticByHoskins"] = () => new TileableWaterCausticByHoskins.Shaders.ProgramFragmentShader(),
			["ToonCloudByAntoineC"] = () => new ToonCloudByAntoineC.Shaders.ProgramFragmentShader(),
			["TopologicaByOtavio"] = () => new TopologicaByOtavio.Shaders.ProgramFragmentShader(),
			["TorusJourneyByFalcao"] = () => new TorusJourneyByFalcao.Shaders.ProgramFragmentShader(),
			["TreeInGrassBySphinx"] = () => new TreeInGrassBySphinx.Shaders.ProgramFragmentShader(),
			["TruchetTentaclesByWaha"] = () => new TruchetTentaclesByWaha.Shaders.ProgramFragmentShader(),
			["Tunnel1ByWaha"] = () => new Tunnel1ByWaha.Shaders.ProgramFragmentShader(),


			["ChromeShaderToyVornoiCubeMapByBenito"] = () => new ChromeShaderToyVornoiCubeMapByBenito.Shaders.ProgramFragmentShader(),
			["VisibleClockByDr2"] = () => new VisibleClockByDr2.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyVRCardboardGrid"] = () => new ChromeShaderToyVRCardboardGrid.Shaders.ProgramFragmentShader(),
			["VoxelPacManByNrx"] = () => new VoxelPacManByNrx.Shaders.ProgramFragmentShader(),
			["VoxelSaturnByGaz"] = () => new VoxelSaturnByGaz.Shaders.ProgramFragmentShader(),
			["VoxelTyreByHoskins"] = () => new VoxelTyreByHoskins.Shaders.ProgramFragmentShader(),
			["VRTestSceneByRaven"] = () => new VRTestSceneByRaven.Shaders.ProgramFragmentShader(),


			["WaterfallByZtri"] = () => new WaterfallByZtri.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyWireframeByYasuo"] = () => new ChromeShaderToyWireframeByYasuo.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyWetStoneByTDM"] = () => new ChromeShaderToyWetStoneByTDM.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyWolfensteinByReinder"] = () => new ChromeShaderToyWolfensteinByReinder.Shaders.ProgramFragmentShader(),

			["XorMountainsByXor"] = () => new XorMountainsByXor.Shaders.ProgramFragmentShader(),
			["XorStormByXor"] = () => new XorStormByXor.Shaders.ProgramFragmentShader(),

			// cube?

			// 

		};
	}
}
