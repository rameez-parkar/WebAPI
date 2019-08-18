pipeline {
    agent any

	parameters{
		string(defaultValue:"anotherapi.sln", description: 'Name of Solution File', name: 'slnFile')
		string(defaultValue:"anotherapi.Test/anotherapi.Test.csproj", description: 'Path of test file', name: 'testFile')
		string(defaultValue:"webapiimage", description: 'Docker Image', name: 'imageName')
		string(defaultValue:"rameezparkar", description: 'Docker Hub Username', name: 'username')
		string(defaultValue:"password", description: 'Docker Hub Password', name: 'password')
		string(defaultValue:"rameezparkar/webapi_repo", description: 'Repository Name', name: 'repositoryName')
		string(defaultValue:"webapi_tag", description: 'Docker image Tag', name: 'tag')
	}
    stages {
		stage('Code Analysis'){
			steps{
				echo 'Performing SonarQube Code Analysis'
				script{
					def scannerhome = tool 'Sonar-Scanner';
					withSonarQubeEnv ('SonarQubeServer'){
						bat "${scannerhome}/bin/sonar-scanner -D sonar.login=admin -D sonar.password=admin"
				}
				}
			}
		}
        stage('Build') {
        	steps{
        		echo 'Building project'
        		bat 'dotnet build %slnFile% -p:Configuration=release -v:n'
                echo 'Finished build'
        	}
        }
        stage('Test') {
        	steps{
        		echo 'Testing project'
        		bat 'dotnet test %testFile%'
                echo 'Finished test'
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
				bat 'docker build --tag=%imageName% --file=Dockerfile .'
				echo 'Docker Image built'
			}
		}
		stage('Docker Login'){
			steps{
				echo 'Logging in to Docker Hub'
				bat 'docker login --username=%username% --password=%password%'
				echo 'Login Complete'
			}
		}
		stage('Docker Push'){
			steps{
				echo 'Pushing image to Docker Hub'
				bat 'docker tag %imageName% %repositoryName%:%tag%'
				bat 'docker push %repositoryName%:%tag%'
				echo 'Image pushed to Docker Hub'
			}
		}
		stage('Docker Pull'){
			steps{
				echo 'Pulling image from Docker Hub'
				bat 'docker pull %repositoryName%:%tag%'
				echo 'Image pulled from Docker Hub'
			}
		}
		stage('Docker Deploy'){
			steps{
				echo 'Started Deploying'
				bat 'docker run -p 8111:11104 --rm %imageName%'
				echo 'Finished Deploying'
			}
		}
    }
    post{
             success{
                 archiveArtifacts artifacts: '**', fingerprint:true
             }
        }
}
