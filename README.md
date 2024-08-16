# facade-template

- facade 框架基于 aspnet core abp 开发的框架，使用 Dapper 支持 Oracle。

- 基于 .net core 最新版本

# 功能

- 基于 abp
- 支持 signalr
- 支持 jwt
- 支持 AutoMapper
- 支持 Quartz
- 支持 Dapper
- 支持 Oracle、SqlServer、Mysql
- 支持 Nlog
- 支持 Redis
- 支持 Swagger 2.0，如： `app.UseSwagger(c => c.SerializeAsV2 = true);`
- 支持 BackgroundJob 后台工作，可以集成 hangfire
- 支持 Modbus 协议 RTU/TCP 传输
- 支持 利用 Socket 使用 MLLP 协议传输 HL7 消息

# 使用

1. 安装 facade 命令工具，`dotnet tool install --global Facade.ToolCLI`
2. 使用命令构建项目 `facade init FacadeDemo`
3. 更改 appsettings.Development.json 文件中的连接字符串
4. 开始使用
