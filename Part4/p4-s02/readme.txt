===========================================
'Unity 디자이너스 바이블' 샘플 프로그램

Part 4／Section 02 'Visual Effect Graph를 활용한 이펙트 만들기'
===========================================

■ 개요 
  Visual Effect Graph를 사용해 물 고리를 모티브로 하는 이펙트 데모입니다.
  책에 기대한 순서에는 씬 안에 오브젝트나 라이트를 배치하는 순서가 포함되어 있으므로 데모 씬을 패키지로 준비했습니다.
  물방울, 물고리, 물포라 각각을 vfx 파일로 만들었습니다.

■ 파일 설명 
  - vfx_graph_sample_2019_2.unitypackage
    Unity2019.2 버전용 데모 씬의 패키지 파일입니다.

  - vfx_graph_sample_2019_3.unitypackage
    Unity2019.3 이후 버전용 데모 씬 패키지 파일입니다.

  - readme.txt
　  이 파일입니다.

■ Unity 프로젝트에 관해
  이 책의 순서 및 Unity 2019.2 버전용 데모 씬은 'Unity 2019.2.9f1'에서 개발했습니다. 또한 위 파일 설명에서의 Unity 2019.3 버전용 데모 씬은 'Unity 2019.3.13f1'에서 개발했습니다.

  다른 버전엣도 사용할 수 있지만, 버전에 따라 변환이 필요한 경우가 있습니다.
  특히 Unity 2019.3부터 HDRP가 정식 릴리스 됨에 따라 'Scene Settings'의 오브젝트를 만들 수 없게 되었습니다.
  때문에, Unity 2019.3 버전용 데모 씬에는 'Scene Settings' 대신 컨텍스트 메뉴에서 'Volumn→Global Volume'을 만들고, 기본 Profile을 클론해서 샘플용으로 새롭게 프로파일을 만들었습니다.
  
  만든 Profile에는 'Exposure' 모드를 'Fix', 'HDRI SKY'의 Hdri Sky를 'DefaultHDRISky', 마찬가지로 'HDRI SKY'의 Exposure를 '-5'로 설정했습니다/

■ 패키지 파일 임포트 방법
[사전 준비]
  패키지 파일을 Unity 프로젝트에 임포트하기 전, HDRP 및 Visual Effect Graph 패키지를 인스톨해야 합니다. HDRP 템플릿 프로젝트를 사용하면 보다 원활하게 확인할 수 있습니다.
  ※ 이 책에서 설명한 순서와 마찬가지로 'Project Settings'에서 'VFX' 탭을 선택해서 셋업을 하시기 바랍니다.。

[패키지 임포트]
  사전 준비를 완료한 프로젝를 연 상태에서, 화면 상단에 메뉴바에서 'Assets→Import Package→Custom Package ...'를 선택합니다.
  표시된 윈도우에서 패키지 파일을 선택하고 'Open'을 클릭합니다. 설치할 파일을 확인하는 화면이 표시되므로 'Import'를 클릭합니다.
  만들어진 'Scenes' 폴더에 있는 'VFX' 씬을 열면 샘플 이펙트를 확인할 수 있습니디.
  ※ 데모 씬의 색상이 책의 색상과 다른 경우에는, 다른 씬을 연 후 다시 데모 씬을 열면 해결되는 경우가 있습니다.ります。

■ 샘플 프로그램 이용시 주의점
  제공하는 데이터는 Unity 학습을 위해 만든 것으로 실사용을 보증하지 않습니다.

■ 샘플 프로그램 저작권에 관해 
  Copyright C 2020 Takashi Todoroki　All rights reserved.
