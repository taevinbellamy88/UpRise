##Each developer clone main repository to their local machine.

##Each developer should create a FEATURE BRANCH off of the DEV branch  nameSchema --- "dev-{feature}"

##make their changes there "dev-{feature}" branch.

##Once the feature is complete---- git status, git add ., git commit -m '{message}' 

##The developer should pull dev latest----  git pull origin dev

##Next, switch to the "dev" branch by running---- "git checkout dev"

##Then, merge the "dev-{feature}" into "dev" ---- "git merge dev-{feature}"

##After resolving any merge conflicts, push the changes to the remote "Dev" repository --- "git push origin dev"

##The developer should then create a pull request to merge their feature branch into the stage branch.

##To push the changes from "Dev" to "Stage" repository you can use the command ---- git checkout "dev" ---- "git push origin stage"

##At least one other developer should review the pull request and merge it.

##Once the stage branch has been tested and approved, it can be merged into the main branch.

##Release the main branch to production.

##To push the changes from "Stage" to "Main"---- git checkout "stage" ---- "git push origin main" 
