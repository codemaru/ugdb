===========================================
'유니티 디자이너스 바이블' 샘플 프로그램

Part 4／Section 05 'ProBuilder를 활용한 모델 만들기와 레벨 디자인'
===========================================

■ 파일/폴더 설명
  - 0406Probuilder_Modeling.unitypackage
    샘플 데이터의 UnityAsset 패키지입니다. Unity 에디터에 로딩해서 사용합니다. 'Assets' 메뉴의 'Import Package' 서브 메뉴 'Custom Package...'에서 로딩하거나, 파일을 직접 Project 윈도우의 Assets에 드래그 앤 드롭해서 사용할 수 있습니다.

    ※ Unity 에디터의 Package Manager에서 ProBuilder를 먼저 인스톨한 뒤 패키지를 로딩해 주십시오.

  - Probuilder_Modeling 폴더
    > Char 폴더 - 캐릭터 Prefab과 연동하는 파일
    > Prop 폴더 - 각 씬에서 사용되는 소도구 Prefab과 관련된 파일
    > Scenes 폴더 - 각 파트에서 만들어진 씬 데이터

  - Probuilder_modeling_readme.txt
    이 파일입니다.

■ 샘플 데이터 사용 방법
  이 책 'Unity 디자이너스 바이블'의 'ProBuilder를 활용한 모델 만들기와 레벨 디자인' 섹션에서 각 파트에서 설명에 사용하는 데이터입니다. 이들 모델 데이터는 캐릭터를 제외하고 모두 ProBuilder에서 만들었습니다.

  - 데이터 확인 방법
    'ProBuilder_Modeling > Scens' 폴더의 각 씬 파일을 더블 클릭하면 모델 데이터를 확인할 수 있습니다.
    '>' 버튼을 클릭해서 재생한 뒤 키보드의 ASWD 키로 캐릭터 이동, 마우스 오른쪽 버튼 드래그로 카메라 시점을 이동할 수 있습니다.

  - 캐릭터 설정 방법
    새롭게 만든 씬은, 다음과 같이 조작해 캐릭터를 배치하면 3인칭 시점에서 확인할 수 있습니다.

    ① 'Project' 윈도우의 'ProBuilder_Modeling > Char' 폴더에 있는 'MegameGirlSp' Prefab을 'Hierarchy' 윈도우 또는 'Scene' 윈도우에 드래그 앤 드롭합니다.
    ② 'Hierarchy' 윈도우의 'Main Camera'를 선택하고, 'Char' 폴더의 C# 프로그램 'Action3dCam'을 'Inspector' 윈도우에 드래그 앤 드롭합니다.
    ③ 'Inspector' 윈도우에 표시된 'Action 3D Cam (Script)' 패널의 'Target'에 'Hierarchy' 윈도우의 'MeganeGirlSp'를 드래그 앤 드롭합니다.


■ 샘플 데이터 / 프로그램 저작권에 관해
©2020 monmoko　All rights reserved.

저작권은 포기하지는 않으나, 모델 데이터나 스크립트는 개인적인 이용에 국한하지 않습니다. 상업적인 이용이나 유료 애플리케이션에도 자유롭게 사용하실 수 있습니다.
