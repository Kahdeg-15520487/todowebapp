<template>
    <div>
        <h1>To-Do List</h1>
        <to-do-form @todo-added="addToDo"></to-do-form>
        <h2 id="list-summary" ref="listSummary" tabindex="-1">{{ listSummary }}</h2>
        <ul aria-labelledby="list-summary" class="stack-large">
            <li v-for="item in ToDoItems" :key="item.id">
                <to-do-item :label="item.label" :done="item.done" :id="item.id"
                    @checkbox-changed="updateDoneStatus(item.id)" @item-deleted="deleteToDo(item.id)"
                    @item-edited="editToDo(item.id, $event)">
                </to-do-item>
            </li>
        </ul>
    </div>
</template>

<script>
import ToDoItem from "./ToDoItem.vue";
import ToDoForm from "./ToDoForm.vue";
import { userInfoStore } from '@/stores/userinfo.ts';
import { fetchWrapper } from '@/_helpers/fetch_wrapper.js';

export default {
    components: {
        ToDoItem,
        ToDoForm,
    },
    data() {
        return {
            userInfo: userInfoStore(),
            ToDoItems: [
            //     { id: uniqueId("todo-"), label: "Learn Vue", done: false },
            //     {
            //         id: uniqueId("todo-"),
            //         label: "Create a Vue project with the CLI",
            //         done: true,
            //     },
            //     { id: uniqueId("todo-"), label: "Have fun", done: true },
            //     { id: uniqueId("todo-"), label: "Create a to-do list", done: false },
            ]
        }
    },
    methods: {
        async fetchToDo(){
            this.ToDoItems.length = 0;
            const res = await fetchWrapper.get(process.env.VUE_APP_API_URL + "/api/todo");
            res.forEach(td => {
                console.log(td);
                let tdobj = JSON.parse(td.content);
                this.ToDoItems.push({
                    id: td.id,
                    label: tdobj.label,
                    done: tdobj.done
                })
            });
        },
        async addToDo(toDoLabel) {
            let tdobj = {
                label: toDoLabel,
                done: false,
            };
            await fetchWrapper.post(process.env.VUE_APP_API_URL + "/api/todo",{
                Content: JSON.stringify(tdobj)
            });
            await this.fetchToDo();
        },
        async updateDoneStatus(toDoId) {
            const toDoToUpdate = this.ToDoItems.find((item) => item.id === toDoId);
            toDoToUpdate.done = !toDoToUpdate.done;
            await fetchWrapper.put(process.env.VUE_APP_API_URL + "/api/todo",{
                Id: toDoId,
                Content: JSON.stringify(toDoToUpdate)
            });
            await this.fetchToDo();
        },
        async deleteToDo(toDoId) {
            await fetchWrapper.delete(process.env.VUE_APP_API_URL + "/api/todo/" + toDoId);
            await this.fetchToDo();
            //https://www.youtube.com/watch?v=JAPDB1qNgOw
        },
        async editToDo(toDoId, newLabel) {
            const toDoToEdit = this.ToDoItems.find((item) => item.id === toDoId);
            toDoToEdit.label = newLabel;
            await fetchWrapper.put(process.env.VUE_APP_API_URL + "/api/todo",{
                Id: toDoId,
                Content: JSON.stringify(toDoToEdit)
            });
            await this.fetchToDo();
        },
    },
    mounted() {
        this.fetchToDo();
    }
}

</script>