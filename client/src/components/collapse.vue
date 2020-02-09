<template>
  <a-layout class="dark">
    <div class="header" @click="this.onToggle" v-bind:class="{ shrink }">
      <a-icon :type="icon" />
      <span>{{ title }}</span>
      <a-icon
        class="arrow"
        v-show="!shrink"
        type="down"
        :rotate="isCollapsed ? 180 : 0"
      />
    </div>
    <template v-if="!(this.shrink || this.isCollapsed)">
      <div class="content">
        <slot></slot>
      </div>
    </template>
  </a-layout>
</template>

<style scoped lang="scss">
.dark {
  background-color: #001529;
}

.header {
  position: relative;
  background-color: #cc18ff80;
  white-space: nowrap;
  color: #fff;
  margin: 4px 0 4px 0;
  padding: 0 16px 0 16px;
  line-height: 40px;
  cursor: pointer;

  .arrow {
    position: absolute;
    top: 16px;
    right: 10px;

    svg {
      width: 10px;
      height: 10px;
    }
  }

  .anticon {
    padding-right: 10px;
  }
}

.shrink {
  padding: 0 32px 0 32px;

  .anticon {
    padding-right: 0px;
  }

  span {
    display: none;
  }

  svg {
    width: 16px;
    height: 16px;
  }
}

.content {
  color: red;
  animation: visibility 0.3s;
}

@keyframes visibility {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}
</style>

<script lang="ts">
import Vue from "vue";

const Collapse = Vue.component("b-collapse", {
  props: {
    icon: String,
    title: String,
    shrink: Boolean
  },
  data: function() {
    return {
      isCollapsed: true
    };
  },
  methods: {
    onToggle: function() {
      if (this.shrink) {
        return;
      }

      this.isCollapsed = !this.isCollapsed;
    }
  }
});

export default Collapse;
</script>
