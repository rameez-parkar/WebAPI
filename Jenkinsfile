pipeline {
    agent any

	parameters{
		string(defaultValue:"anotherapi.sln", description: 'name of solution file', name: 'slnFile')
		string(defaultValue:"anotherapi.Test/anotherapi.Test.csproj", description: 'path of test file', name: 'testFile')
		string(defaultValue:"webapiimage", description: 'docker image', name: 'imageFile')
	}
    stages {
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
		stage('Docker Deploy'){
			steps{
				echo 'Start Deploying'
				bat 'docker build -t %imageFile% -f Dockerfile .'
				bat 'docker run -p 8111:11104 %imageFile%'
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
