pipeline {
    agent any

	parameters{
		string(defaultValue:"anotherapi.sln", description: 'Name of Solution File', name: 'slnFile')
		string(defaultValue:"anotherapi.dll", description: "Solution Dll File", name:"slnDll")
		string(defaultValue:"anotherapi.Test/anotherapi.Test.csproj", description: 'Path of test file', name: 'testFile')
		string(defaultValue:"webapiimage", description: 'Docker Image', name: 'imageName')
		string(defaultValue:"webapi_repo", description: 'Repository Name', name: 'repositoryName')
		string(defaultValue:"rameezparkar", description: 'Registry Name', name: 'registryName')
		string(defaultValue:"webapi_tag", description: 'Docker image Tag', name: 'tag')
		string(defaultValue:"80", description: 'Docker port', name: 'dockerPort')
		string(defaultValue:"8111", description: 'Local port', name: 'localPort')
		string(defaultValue:"Rameez:basicwebapi", description: 'Sonarqube Project Key', name: 'projectKey')
	}
    stages {
        stage('Build') {
        	steps{
        		echo 'Building project'
        		bat 'dotnet build %slnFile% -p:Configuration=release -v:n'
                echo 'Finished build'
        	}
        }
      
        stage('Publish') {
        	steps{
        		echo 'Publishing project'
        		bat 'dotnet publish -c Release -o Publish'
                echo 'Finished publish'
        	}
        }
		stage('Docker Build'){
			steps{
				echo 'Start Building Docker image'
				bat 'docker build --build-arg PublishPath=%PublishPath% --tag=%imageName% --file=Dockerfile .'
				echo 'Docker Image built'
			}
		}
		
		stage('Docker Deploy'){
			steps{
				echo 'Started Deploying'
				bat 'docker run  -p %localPort%:%dockerPort%  -e SOLUTION_DLL=%slnDll% %imageName%'
			}
		}
    }
    post{
             always{
                 cleanWs()
             }
        }
}
