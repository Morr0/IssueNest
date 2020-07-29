<template>
    <div v-if="project">
        <li>Project: {{project.name}}</li>
    </div>
</template>

<script>
export default {
    props: {
        project: {},
    }
}
</script>

<style>

</style>