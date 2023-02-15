# A la orilla del rio
## Branch protection rules
- Main branch: <code>main</code>
- Develop branch: <code>develop</code>
- Release branches: <code>release/*</code>
- Hotfix branches: <code>hotfix/*</code>
- Feature branches: <code>feature/*</code>
## Branch naming conventions
- Main branch: <code>main</code>
- Develop branch: <code>development</code>
- Release branches: <code>release/*</code>
- Bugfix branches: <code>bugfix/*</code>
- Feature branches: <code>feature/*</code>
## How to push to the repository
- Create a new branch from the <code>development</code> branch
- Push the new branch to the repository
- Create a pull request to the <code>development</code> branch
- Merge the pull request
- Delete the branch
## Pull request conventions
### Pull request to the <code>main</code> branch
- Requires at least two approvals from code owners
- Only the code owner can merge the pull request
- Only merge pull requests from the <code>development</code> branch
### Pull request to the <code>development</code> branch
- Requires at least one approval from another developer
- Requires a linear history (no merge commits)

