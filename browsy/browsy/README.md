<?xml version="1.0"?>
<package xmlns="http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd">
    <metadata>
    <id>chris17453.browsy</id>
    <version>1.0.0</version>
    <authors>Charles Watkins</authors>
    <owners>chris17453</owners>
    <licenseUrl>http://opensource.org/licenses/MS-PL</licenseUrl>
    <projectUrl>http://github.com/chris17453/browsy</projectUrl>
    <iconUrl>https://github.com/chris17453/browsy/browsy.icon.png</iconUrl>
    <requireLicenseAcceptance>false</requireLicenseAcceptance>
    <releaseNotes>Initial Release</releaseNotes>
    <description>A web file browser for server files. Using jQuery and webAPI. Tested in Mono on Linux.</description>
    <copyright>Copyright ©2018 Charles Watkins</copyright>
    <tags>jQuery webAPI Server File Browser</tags>
    <dependencies>
		
        <dependency id="Microsoft.AspNet.WebApi.Client" version="5.2.3" />
        <dependency id="Microsoft.AspNet.WebApi.Core" version="5.2.3" />
        <dependency id="Newtonsoft.Json" version="6.0" />
    </dependencies>
  </metadata>
    <files>
    <file src="./bin/Release/browsy.dll" target="lib\45\browsy.dll" />
    <file src="./README.md" target="" />
</files>
</package>
