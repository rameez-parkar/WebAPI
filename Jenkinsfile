pipeline {
    agent any

	parameters{
		string(defaultValue:"anotherapi.sln", description: 'name of solution file', name: 'slnFile')
		string(defaultValue:"anotherapi.Test/anotherapi.Test.csproj", description: 'path of test file', name: 'testFile')
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
        		bat 'dotnet publish'
                echo 'Finished publish'
        	}
        }
    }
    post{
             success{
                 archiveArtifacts artifacts: '**', fingerprint:true
             }
        }
}
