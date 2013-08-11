  <ItemGroup>
    <Content Include="opensource\readme.txt" />
    <Content Include="X:\opensource\github\SiON\src\**\*.*">
      <Link>opensource\github\SiON\%(RecursiveDir)%(FileName)%(Extension)</Link>
    </Content>
  </ItemGroup>

https://github.com/keim/SiON/

import org.si.sion.SiONDriver;
import org.si.sion.midi.SMFData;

[Embed(source="test.mid", mimeType="application/octet-stream")]
var Test:Class;

var smfData:SMFData = new SMFData();
var driver:SiONDriver = new SiONDriver();
smfData.loadBytes(new Test);
driver.play(smfData);

http://stackoverflow.com/questions/2035948/play-midi-files-in-flash

https://sites.google.com/site/sioncenter/example
https://sites.google.com/site/sioncenter/
http://keim-at-si.blogspot.jp/
http://keim-at-si.blogspot.jp/2012/08/sion-v065-is-available-now.html
http://www.libspark.org/htdocs/as3/sion/index.html
http://wonderfl.net/c/4IH3

!!! what if the source is so broken we should not fix it? can we revert to swc?

add import flash.events.*;
add import org.si.sion.sequencer.SiMMLTrack;


Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorVRC6.as(77): col: 48 Error: Type was not found or was not a compile-time constant: SiMMLTrack.

        internal function initializeTone(track:SiMMLTrack, chNum:int, bufferIndex:int) : int
                                               ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorVRC6.as(117): col: 44 Error: Type was not found or was not a compile-time constant: SiMMLTrack.

        internal function selectTone(track:SiMMLTrack, voiceIndex:int) : MMLSequence
                                           ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorBase.as(75): col: 46 Error: Type was not found or was not a compile-time constant: SiMMLTrack.

        public function initializeTone(track:SiMMLTrack, chNum:int, bufferIndex:int) : int
                                             ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorBase.as(118): col: 42 Error: Type was not found or was not a compile-time constant: SiMMLTrack.

        public function selectTone(track:SiMMLTrack, voiceIndex:int) : MMLSequence
                                         ^

Y:\opensource\github\SiON\org\si\midi\MIDIPlayer.as(155): col: 48 Error: Type was not found or was not a compile-time constant: Event.

        static private function _waitAndPlay(e:Event) : void
                                               ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorFM.as(23): col: 51 Error: Type was not found or was not a compile-time constant: SiMMLTrack.

        override public function selectTone(track:SiMMLTrack, voiceIndex:int) : MMLSequence
                                                  ^


looks like every opensource project has some source level issues.
this time file names are different but content is the same.
SiMMLSimulatorKS.as
SiMMLSimulatorSampler.as
SiMMLSimulatorSID.as

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSID.as(22): col: 29 Error: A conflict exists with definition SELECT_TONE_NOP in namespace internal.

        static public const SELECT_TONE_NOP   :int = 0;
                            ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSID.as(23): col: 29 Error: A conflict exists with definition SELECT_TONE_NORMAL in namespace internal.

        static public const SELECT_TONE_NORMAL:int = 1;
                            ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSID.as(24): col: 29 Error: A conflict exists with definition SELECT_TONE_FM in namespace internal.

        static public const SELECT_TONE_FM    :int = 2;
                            ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSID.as(31): col: 22 Error: A conflict exists with definition type in namespace internal.

        public   var type:int;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSID.as(32): col: 22 Error: A conflict exists with definition _selectToneType in namespace internal.

        internal var _selectToneType:int;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSID.as(33): col: 22 Error: A conflict exists with definition _pgTypeList in namespace internal.

        internal var _pgTypeList:Vector.<int>;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSID.as(34): col: 22 Error: A conflict exists with definition _ptTypeList in namespace internal.

        internal var _ptTypeList:Vector.<int>;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSID.as(35): col: 22 Error: A conflict exists with definition _initVoiceIndex in namespace internal.

        internal var _initVoiceIndex:int;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSID.as(36): col: 22 Error: A conflict exists with definition _voiceIndexTable in namespace internal.

        internal var _voiceIndexTable:Vector.<int>;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSID.as(37): col: 22 Error: A conflict exists with definition _channelType in namespace internal.

        internal var _channelType:int;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSID.as(38): col: 22 Error: A conflict exists with definition _isSuitableForFMVoice in namespace internal.

        internal var _isSuitableForFMVoice:Boolean;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSID.as(39): col: 22 Error: A conflict exists with definition _defaultOpeCount in namespace internal.

        internal var _defaultOpeCount:int;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSID.as(40): col: 22 Error: A conflict exists with definition _table in namespace internal.

        private  var _table:SiOPMTable;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSID.as(47): col: 18 Error: Duplicate function definition.

        function SiMMLModuleSimulatorBase(type:int, offset:int, length:int, step:int, channelCount:int)
                 ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSID.as(77): col: 27 Error: Duplicate function definition.

        internal function initializeTone(track:SiMMLTrack, chNum:int, bufferIndex:int) : int
                          ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSID.as(117): col: 27 Error: Duplicate function definition.

        internal function selectTone(track:SiMMLTrack, voiceIndex:int) : MMLSequence
                          ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSampler.as(22): col: 29 Error: A conflict exists with definition SELECT_TONE_NOP in namespace internal.

        static public const SELECT_TONE_NOP   :int = 0;
                            ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSampler.as(23): col: 29 Error: A conflict exists with definition SELECT_TONE_NORMAL in namespace internal.

        static public const SELECT_TONE_NORMAL:int = 1;
                            ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSampler.as(24): col: 29 Error: A conflict exists with definition SELECT_TONE_FM in namespace internal.

        static public const SELECT_TONE_FM    :int = 2;
                            ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSampler.as(31): col: 22 Error: A conflict exists with definition type in namespace internal.

        public   var type:int;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSampler.as(32): col: 22 Error: A conflict exists with definition _selectToneType in namespace internal.

        internal var _selectToneType:int;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSampler.as(33): col: 22 Error: A conflict exists with definition _pgTypeList in namespace internal.

        internal var _pgTypeList:Vector.<int>;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSampler.as(34): col: 22 Error: A conflict exists with definition _ptTypeList in namespace internal.

        internal var _ptTypeList:Vector.<int>;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSampler.as(35): col: 22 Error: A conflict exists with definition _initVoiceIndex in namespace internal.

        internal var _initVoiceIndex:int;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSampler.as(36): col: 22 Error: A conflict exists with definition _voiceIndexTable in namespace internal.

        internal var _voiceIndexTable:Vector.<int>;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSampler.as(37): col: 22 Error: A conflict exists with definition _channelType in namespace internal.

        internal var _channelType:int;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSampler.as(38): col: 22 Error: A conflict exists with definition _isSuitableForFMVoice in namespace internal.

        internal var _isSuitableForFMVoice:Boolean;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSampler.as(39): col: 22 Error: A conflict exists with definition _defaultOpeCount in namespace internal.

        internal var _defaultOpeCount:int;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSampler.as(40): col: 22 Error: A conflict exists with definition _table in namespace internal.

        private  var _table:SiOPMTable;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSampler.as(47): col: 18 Error: Duplicate function definition.

        function SiMMLModuleSimulatorBase(type:int, offset:int, length:int, step:int, channelCount:int)
                 ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSampler.as(77): col: 27 Error: Duplicate function definition.

        internal function initializeTone(track:SiMMLTrack, chNum:int, bufferIndex:int) : int
                          ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorSampler.as(117): col: 27 Error: Duplicate function definition.

        internal function selectTone(track:SiMMLTrack, voiceIndex:int) : MMLSequence
                          ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorKS.as(22): col: 29 Error: A conflict exists with definition SELECT_TONE_NOP in namespace internal.

        static public const SELECT_TONE_NOP   :int = 0;
                            ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorKS.as(23): col: 29 Error: A conflict exists with definition SELECT_TONE_NORMAL in namespace internal.

        static public const SELECT_TONE_NORMAL:int = 1;
                            ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorKS.as(24): col: 29 Error: A conflict exists with definition SELECT_TONE_FM in namespace internal.

        static public const SELECT_TONE_FM    :int = 2;
                            ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorKS.as(31): col: 22 Error: A conflict exists with definition type in namespace internal.

        public   var type:int;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorKS.as(32): col: 22 Error: A conflict exists with definition _selectToneType in namespace internal.

        internal var _selectToneType:int;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorKS.as(33): col: 22 Error: A conflict exists with definition _pgTypeList in namespace internal.

        internal var _pgTypeList:Vector.<int>;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorKS.as(34): col: 22 Error: A conflict exists with definition _ptTypeList in namespace internal.

        internal var _ptTypeList:Vector.<int>;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorKS.as(35): col: 22 Error: A conflict exists with definition _initVoiceIndex in namespace internal.

        internal var _initVoiceIndex:int;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorKS.as(36): col: 22 Error: A conflict exists with definition _voiceIndexTable in namespace internal.

        internal var _voiceIndexTable:Vector.<int>;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorKS.as(37): col: 22 Error: A conflict exists with definition _channelType in namespace internal.

        internal var _channelType:int;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorKS.as(38): col: 22 Error: A conflict exists with definition _isSuitableForFMVoice in namespace internal.

        internal var _isSuitableForFMVoice:Boolean;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorKS.as(39): col: 22 Error: A conflict exists with definition _defaultOpeCount in namespace internal.

        internal var _defaultOpeCount:int;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorKS.as(40): col: 22 Error: A conflict exists with definition _table in namespace internal.

        private  var _table:SiOPMTable;
                     ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorKS.as(47): col: 18 Error: Duplicate function definition.

        function SiMMLModuleSimulatorBase(type:int, offset:int, length:int, step:int, channelCount:int)
                 ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorKS.as(77): col: 27 Error: Duplicate function definition.

        internal function initializeTone(track:SiMMLTrack, chNum:int, bufferIndex:int) : int
                          ^

Y:\opensource\github\SiON\org\si\sion\sequencer\simulator\SiMMLSimulatorKS.as(117): col: 27 Error: Duplicate function definition.

        internal function selectTone(track:SiMMLTrack, voiceIndex:int) : MMLSequence
                          ^
