<template>
  <a-layout>
    <a-list itemLayout="vertical" :dataSource="data">
      <template slot="header">
        <p>Hello header!</p>
      </template>
      <a-list-item slot="renderItem" slot-scope="item">
        <template slot="extra">
          <img
            width="275"
            alt="logo"
            src="https://gw.alipayobjects.com/zos/rmsportal/mqaQswcyDLcXyDKnZfES.png"
          />
        </template>
        <template slot="actions" v-for="{ type, text } in actions">
          <span :key="type">
            <a-icon :type="type" style="margin-right: 8px" />
            {{ text }}
          </span>
        </template>
        <a-list-item-meta
          description="Ant Design, a design language for background applications, is refined by Ant UED Team"
        >
          <template slot="title">
            <a href="https://www.antdv.com/">{{ item.title }}</a>
          </template>
          <template slot="avatar">
            <a-avatar
              src="https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png"
            />
          </template>
        </a-list-item-meta>
        <p>
          Hello this is random text what I am trying to write so please be
          careful that to do about it! thank you in advance and see you
          tomorrow!
        </p>
      </a-list-item>
      <template slot="footer">
        <p>Hello footer!</p>
      </template>
    </a-list>
  </a-layout>
</template>

<script lang="ts">
import Vue from "vue";

const Home = Vue.extend({
  data: function() {
    return {
      actions: [
        { type: "star-o", text: "156" },
        { type: "like-o", text: "156" },
        { type: "message", text: "2" }
      ],
      data: [
        {
          title: "Ant Design Title 1"
        },
        {
          title: "Ant Design Title 2"
        },
        {
          title: "Ant Design Title 3"
        },
        {
          title: "Ant Design Title 4"
        }
      ]
    };
  }
});

export default Home;
</script>
