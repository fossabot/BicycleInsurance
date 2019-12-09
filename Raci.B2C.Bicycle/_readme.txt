nuget sources add -Name "Satalyst" -Source "http://satalyst-teamcity.cloudapp.net/httpAuth/app/nuget/v1/FeedService.svc/" -UserName "[USERNAME]" -Password "[PASSWORD]"

install-package Raci.B2CPlatform -Source Satalyst -ProjectName Raci.B2C.Bicycle
update-package Raci.B2CPlatform -Source Satalyst -ProjectName Raci.B2C.Bicycle
