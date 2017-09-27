publish:
	@cd $(project) && rm -rf bin/Release/
	@cd $(project) && dotnet build -c Release
	@cd $(project) && dotnet nuget push bin/Release/$(project).*.nupkg -k d170e3d1-da50-42d4-b4b0-670bb7fe7a49 -s https://nuget.org