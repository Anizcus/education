import { Service } from './service';
import { NameServiceModel } from '@/models/services/name.service.model';

interface NameGroupModel {
   id: number;
   label: string;
   options: NameServiceModel[];
}

const TypeService = {
   getTypes: (): Promise<NameGroupModel[]> => {
      return Service.get("type/all");
    },
}

export { TypeService, NameGroupModel };