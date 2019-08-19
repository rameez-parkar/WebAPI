pipeline {
    agent any

	parameters{
		string(defaultValue:"anotherapi.sln", description: 'Name of Solution File', name: 'slnFile')
		string(defaultValue:"anotherapi.Test/anotherapi.Test.csproj", description: 'Path of test file', name: 'testFile')
		string(defaultValue:"webapiimage", description: 'Docker Image', name: 'imageName')
		string(defaultValue:"rameezparkar/webapi_repo", description: 'Repository Name', name: 'repositoryName')
		string(defaultValue:"webapi_tag", description: 'Docker image Tag', name: 'tag')
		string(defaultValue:"8111", description: 'Docker port', name: 'dockerPort')
		string(defaultValue:"11104", description: 'Local port', name: 'localPort')
		string(defaultValue:"Rameez:basicwebapi", description: 'Sonarqube Project Key', name: 'projectKey')
		string(defaultValue:"http://localhost:9000", description: 'Sonar Host Url', name: 'sonarHostUrl')
	}
    stages {
        stage('Build') {
        	steps{
        		echo 'Building project'
        		bat 'dotnet build %slnFile% -p:Configuration=release -v:n'
                echo 'Finished build'
        	}
        }
		stage('Code Analysis'){
			steps{
				echo 'Performing SonarQube Code Analysis'
				script{
					def scannerhome = tool 'Sonar-Scanner'
					withSonarQubeEnv ('SonarQubeServer'){
						withCredentials([usernamePassword(credentialsId: '43ecc876-fb21-4289-aed9-e8ad51aae1e2', passwordVariable: 'password', usernameVariable: 'username')]){
							bat 'dotnet C:/Users/rparkar/Downloads/sonar-scanner-msbuild-4.6.2.2108-netcoreapp2.0/SonarScanner.MSBuild.dll begin /key:%projectKey% /d:sonar.host.url=%sonarHostUrl%  /d:sonar.login=%username% /d:sonar.password=%password%'
							bat 'dotnet build'
							bat 'dotnet C:/Users/rparkar/Downloads/sonar-scanner-msbuild-4.6.2.2108-netcoreapp2.0/SonarScanner.MSBuild.dll end /d:sonar.login=%username% /d:sonar.password=%password%'
						}
					}
				}
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
				bat 'docker run -d -p %dockerPort%:%localPort% --rm %imageName%'
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
