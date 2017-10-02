token = `cat ../NuGet.token`

publish:
	@cd $(project) && rm -rf bin/Release/
	@cd $(project) && dotnet build -c Release
	@cd $(project) && dotnet nuget push bin/Release/$(project).*.nupkg -k $(token) -s https://nuget.org