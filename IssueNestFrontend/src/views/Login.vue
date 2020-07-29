<template>
    <form action="">
        <label for="email">Email: </label>
        <input type="email" name="email" placeholder="example@example.example" :v-model="email">
        <label for="password">Password: </label>
        <input type="password" placeholder="********" :v-model="password">
        <button type="button" @click.prevent="login">Login</button>
    </form>
</template>

<script>
export default {
    data(){
        return {
            email: "",
            password: "",
        };
    },
    methods: {
        login: async function (){

        }
    }
}
</script>

<style>

</style>