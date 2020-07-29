<template>
    <div>
        Projects: 
        <div v-if="projects">
            <Project v-for="project in projects" :key="project.id" :project="project" />
        </div>
        <div v-else>
            There are no projects
        </div>
        
    </div>
</template>

<script>
import Project from "@/components/Project.vue";

export default {
    components: {
        Project
    },
    props: {
        user: {},
    },
    data(){
        return {
            projects: []
        };
    },
    mounted(){
        // Get user's projects
        fetch(`https://localhost:5001/api/project/user/${this.user.id}`)
        .then(async (res) => {
            if (res.status === 200){
                return this.projects = await res.json();
            }

            alert("An error has occured");
        });
        
    }
}
</script>

<style>

</style>