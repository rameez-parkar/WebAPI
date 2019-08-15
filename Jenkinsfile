pipeline {
    agent any
    stages {
        stage('Build') {
        	steps{
        		echo 'Building project'
        		sh 'dotnet build anotherapi.sln -p:Configuration=release -v:n'
            echo 'Finished build'
        	}
        }
        stage('Test') {
        	steps{
        		echo 'Testing project'
        		sh 'dotnet test anotherapi.Test/anotherapi.Test.csproj'
            echo 'Finished test'
        	}
        }
        stage('Publish') {
        	steps{
        		echo 'Publishing project'
        		sh 'dotnet publish'
            echo 'Finished publish'
        	}
        }
        stage('Deploy') {
        	steps{
        		echo 'Deploy project'
        		sh 'dotnet anotherapi/bin/Release/netcoreapp2.2/anotherapi.dll'
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
