pipeline {
    agent any
    stages {
        stage('Build') {
        	steps{
        		echo 'Building project'
        		bat 'dotnet build anotherapi.sln -p:Configuration=release -v:n'
            echo 'Finished build'
        	}
        }
        stage('Test') {
        	steps{
        		echo 'Testing project'
        		bat 'dotnet test anotherapi.Test/anotherapi.Test.csproj'
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
        stage('Deploy') {
        	steps{
        		echo 'Deploy project'
        		bat 'dotnet anotherapi/bin/Release/netcoreapp2.2/anotherapi.dll'
            echo 'Finished deploy'
        	}
        }
    }
    post{
             success{
                 archiveArtifacts artifacts: '**', fingerprint:true
             }
        }
}
