prepare-front:
	rm -rf ../badengine-editor/extraResources/common/badengine-lib/Badengine
	rm -rf ../badengine-editor/extraResources/common/badengine-lib/badengine.csproj
	cp -R Badengine ../badengine-editor/extraResources/common/badengine-lib/
	cp badengine.csproj ../badengine-editor/extraResources/common/badengine-lib/
