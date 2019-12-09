MKDIR Raci.B2C.Bicycle\ClientApi
:: CALL packages\autorest.0.12.0\tools\autorest -CodeGenerator CSharp -Modeler Swagger -Input "https://devbicycleinsuranceracisvc.azurewebsites.net/swagger/docs/v1" -Namespace "Raci.B2C.Bicycle.ClientApi" -OutputDirectory "Raci.B2C.Bicycle\ClientApi"
CALL packages\autorest.0.12.0\tools\autorest -CodeGenerator CSharp -Modeler Swagger -Input "http://localhost:8771/swagger/docs/v1" -Namespace "Raci.B2C.Bicycle.ClientApi" -OutputDirectory "Raci.B2C.Bicycle\ClientApi"

pause