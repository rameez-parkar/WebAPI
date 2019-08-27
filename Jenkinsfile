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
		stage('Docker Login'){
			steps{
				withCredentials([usernamePassword(credentialsId: '433624c2-9a39-483d-aad6-48526cf306f0', passwordVariable: 'password', usernameVariable: 'username')]){
					echo 'Logging in to Docker Hub'
					bat 'docker login --username=%username% --password=%password%'
					echo 'Login Complete'
				}
			}
		}
		stage('Docker Push'){
			steps{
				echo 'Pushing image to Docker Hub'
				bat 'docker tag %imageName% %registryName%/%repositoryName%:%tag%'
				bat 'docker push %registryName%/%repositoryName%:%tag%'
				bat 'docker rmi %imageName%'
				echo 'Image pushed to Docker Hub'
			}
		}
		stage('Docker Pull'){
			steps{
				echo 'Pulling image from Docker Hub'
				bat 'docker pull %registryName%/%repositoryName%:%tag%'
				echo 'Image pulled from Docker Hub'
			}
		}
		stage('Docker Deploy'){
			steps{
				echo 'Started Deploying'
				bat 'docker run -e SOLUTION_DLL ="%slnDll%" -p %localPort%:%dockerPort% %registryName%/%repositoryName%:%tag%'
			}
		}
    }
    post{
             always{
                 cleanWs()
             }
        }
}
