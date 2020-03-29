interface FormRefModel extends Vue {
  validate: () => Promise<boolean>;
  validateField: (field: string) => void;
}

export { FormRefModel };
