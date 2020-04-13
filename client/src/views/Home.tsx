
import Vue from "vue";
import Component from "vue-class-component";
import { VNode } from 'vue/types/umd';
import { mapActions, ActionMethod } from 'vuex';

@Component({
   methods: {
     ...mapActions("lesson", {
       getCategories: "getCategories"
     })
   }
 })
class Home extends Vue {
   private getCategories!: ActionMethod;

   public mounted() {
      this.getCategories().then(res => {
         console.log(res);
      });
   }

   public render(): VNode {
      return <el-row>
         <el-col>
            <el-card>Hello World!</el-card>
         </el-col>
      </el-row>
   }
}

export default Home;