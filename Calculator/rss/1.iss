; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Calculator"
#define MyAppVersion "1.0"
#define MyAppPublisher "Nickolay Gureyev"
#define MyAppURL "https://github.com/Chocobo1983/"
#define MyAppExeName "Calculator.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{B2F8F6C5-FFA2-4E25-BDCB-928633348CB3}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputBaseFilename=Setup
SetupIconFile=D:\����������������\����������������\���\WPF\new\HW\HW1\Calculator\rss\Calculator.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "D:\����������������\����������������\���\WPF\new\HW\HW1\Calculator\bin\Release\app.publish\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\����������������\����������������\���\WPF\new\HW\HW1\Calculator\bin\Release\Calculator.application"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\����������������\����������������\���\WPF\new\HW\HW1\Calculator\bin\Release\Calculator.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\����������������\����������������\���\WPF\new\HW\HW1\Calculator\bin\Release\Calculator.exe.manifest"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\����������������\����������������\���\WPF\new\HW\HW1\Calculator\bin\Release\Calculator.pdb"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

