interface FormRefModel extends Vue {
  validate: () => Promise<boolean>;
}

export { FormRefModel };
